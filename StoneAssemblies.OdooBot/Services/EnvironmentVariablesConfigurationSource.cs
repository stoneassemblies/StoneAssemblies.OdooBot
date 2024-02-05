namespace StoneAssemblies.OdooBot.Services
{
    /// <summary>
    /// The environment variables configuration source.
    /// </summary>
    public class EnvironmentVariablesConfigurationSource : IConfigurationSource
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
        /// Initializes a new instance of the <see cref="EnvironmentVariablesConfigurationSource"/> class.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <param name="sectionSeparator">
        /// The section separator.
        /// </param>
        public EnvironmentVariablesConfigurationSource(string prefix, string sectionSeparator)
        {
            this.prefix = prefix;
            this.sectionSeparator = sectionSeparator;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigurationProvider"/>.
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EnvironmentVariablesConfigurationProvider(this.prefix, this.sectionSeparator);
        }
    }
}