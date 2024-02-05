// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryConfigurationProvider.cs" company="Port Hope Investment S.A.">
// Copyright © 2023 - 2023 Port Hope Investment S.A. development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.OdooBot.Services
{
    public class InMemoryConfigurationProvider(string prefix, string sectionSeparator, IDictionary<string, string> configurationData)
        : ConfigurationProvider
    {
        public override void Load()
        {
            foreach (var key in configurationData.Keys)
            {
                var trimmedKey = key.Replace($"{prefix}{sectionSeparator}", string.Empty);

                Data[trimmedKey.Replace(sectionSeparator, ":")] = (string?) configurationData[key];
            }
        }
    }
}