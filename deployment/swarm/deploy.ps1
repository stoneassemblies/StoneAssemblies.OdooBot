function Add-ReplicableServiceCreate([System.Text.StringBuilder] $commandBuilder, [string] $serviceName, [int] $replicas) {
    $commandBuilder.Append("docker service create --with-registry-auth --name ${serviceName} --detach --replicas ${replicas} --restart-condition on-failure ")
}

function Add-ContainerCreate()
{
    param(
        [Parameter(Mandatory=$true)][System.Text.StringBuilder] $commandBuilder,
        [Parameter(Mandatory=$true)][string] $containerName,
        [Parameter(Mandatory=$false)][int] $targetPort = -1,
        [Parameter(Mandatory=$false)][int] $publishedPort = -1
    )
    if ($targetPort -eq -1 -or $publishedPort -eq -1) 
    {
        $commandBuilder.Append("docker run --name ${containerName} --detach --restart always ")
    } 
    else 
    {
        $commandBuilder.Append("docker run --name ${containerName} -p ${targetPort}:${publishedPort} --detach --restart always ")
    }
}

function Add-HostServiceCreate([System.Text.StringBuilder] $commandBuilder, [string] $serviceName, [int] $targetPort, [int] $publishedPort) 
{
    $commandBuilder.Append("docker service create --with-registry-auth --name $serviceName --detach --restart-condition on-failure --mode global --publish mode=host,target=$targetPort,published=$publishedPort ");
}

$dockerRepositoryProxy = [System.Environment]::GetEnvironmentVariable("DOCKER_REPOSITORY_PROXY")

Write-Host "Installing Services ..."

Write-Host "=================================="
Write-Host "Loading Images ..."
Write-Host "=================================="
$dockerImagesPath = [System.IO.Path]::Combine($PSScriptRoot, "../docker")
if (Test-Path $dockerImagesPath)
{
	Get-ChildItem -Path $dockerImagesPath -Filter *.tar | ForEach-Object { docker load -i $_.FullName }
}
# PUSH into local repository?


Write-Host "=================================="
Write-Host "Installing Odoo-Bot ..."
Write-Host "=================================="

docker rm odoo-bot
$commandBuilder = [System.Text.StringBuilder]::new()
Add-ContainerCreate $commandBuilder "odoo-bot"  -targetPort 80 -publishedPort 6000

$secrets = @()
$environmentVariables = @("BOT_ODOO_APIURL", "BOT_ODOO_DATABASE", "BOT_ODOO_USERNAME", "BOT_ODOO_PASSWORD")
foreach($environmentVariable in $environmentVariables)
{
	$environmentVariableValue = [System.Environment]::GetEnvironmentVariable($environmentVariable)
	foreach($secret in $secrets)
	{
		$secretValue = [System.Environment]::GetEnvironmentVariable($secret)
		$environmentVariableValue = $environmentVariableValue.Replace("%$secret%", $secretValue)
	}

	$commandBuilder.Append("--env $environmentVariable=`"$environmentVariableValue`" ")
}

# required for service discovery.
$commandBuilder.Append("--env APP_INSTANCE=`"0`" ")
$commandBuilder.Append("-v /data/odoo-bot:/app/data ")
$commandBuilder.Append("$dockerRepositoryProxy/stone-assemblies-odoo-bot:${VERSION_NUMBER}")
$command = $commandBuilder.ToString()

Write-Host $command

$scriptBlock = [ScriptBlock]::Create($command)
Invoke-Command $scriptBlock
if ($LASTEXITCODE -ne 0) {
	Write-Error 'Error creating docker swarm stone-assemblies-odoo-bot' -ErrorAction Stop
}
