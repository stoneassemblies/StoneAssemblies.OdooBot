namespace StoneAssemblies.OdooBot.Services
{
    /// <summary>
    /// The environment variables configuration provider.
    /// </summary>
    public class EnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// The prefix.
        /// </summary>
        private readonly string prefix;

        /// <summary>
        /// The section separator.
        /// </summary>
        private readonly string sectionSeparator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentVariablesConfigurationProvider"/> class.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <param name="sectionSeparator">
        /// The separator.
        /// </param>
        public EnvironmentVariablesConfigurationProvider(string prefix, string sectionSeparator)
        {
            this.prefix = prefix;
            this.sectionSeparator = sectionSeparator;
        }

        /// <inheritdoc/>
        public override void Load()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();

            foreach (string key in environmentVariables.Keys)
            {
                var trimmedKey = key.Replace($"{this.prefix}{this.sectionSeparator}", string.Empty);

                this.Data[trimmedKey.Replace(this.sectionSeparator, ":")] = (string?)environmentVariables[key];
            }
        }
    }
}