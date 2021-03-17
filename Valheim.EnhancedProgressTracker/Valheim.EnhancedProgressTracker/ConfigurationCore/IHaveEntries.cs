using System.Collections.Generic;

namespace Valheim.EnhancedProgressTracker.ConfigurationCore
{
    public interface IHaveEntries
    {
        Dictionary<string, IConfigurationEntry> Entries { get; set; }
    }
}
