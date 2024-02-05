// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryConfigurationSource.cs" company="Port Hope Investment S.A.">
// Copyright © 2023 - 2023 Port Hope Investment S.A. development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.OdooBot.Services
{
    public class InMemoryConfigurationSource(string prefix, string sectionSeparator,
        IDictionary<string, string> configurationData) : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new InMemoryConfigurationProvider(prefix, sectionSeparator, configurationData);
        }
    }
}