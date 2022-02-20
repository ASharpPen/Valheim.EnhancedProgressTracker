using System.Collections.Generic;

namespace Valheim.EnhancedProgressTracker.ConfigurationCore
{
    internal interface IHaveEntries
    {
        Dictionary<string, IConfigurationEntry> Entries { get; set; }
    }
}
