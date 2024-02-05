var buildConfiguration = Argument("Configuration", "Release");

string NuGetVersionV2 = "";
string SolutionFileName = "src/StoneAssemblies.OdooBot.sln";

string[] DockerFiles = new[]
{
	"./deployment/docker/StoneAssemblies.OdooBot/Dockerfile",
};

string[] OutputImages = new[]
{
	"stone-assemblies-odoo-bot",
};

string[] ComponentProjects = new string[]
{
};

string[] ExecProjects = new string[]
{
};

var ExtraFiles = new System.Collections.Generic.Dictionary<string, string>();


var ZipFiles = new System.Collections.Generic.Dictionary<string, (string Path, string Pattern, string[] ExclusionPatterns)>();


string[] TestProjects =
{
};

var NuGetFiles = new System.Collections.Generic.Dictionary<string, string>();

string SonarProjectKey = "";
string SonarOrganization = "";