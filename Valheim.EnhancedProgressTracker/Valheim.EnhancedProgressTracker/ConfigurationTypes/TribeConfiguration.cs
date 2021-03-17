using System;
using Valheim.EnhancedProgressTracker.ConfigurationCore;

namespace Valheim.EnhancedProgressTracker.ConfigurationTypes
{
    [Serializable]
    public class TribeConfiguration : ConfigurationGroup<TribeMember>
    {
    }

    [Serializable]
    public class TribeMember : ConfigurationSection
    {
        private int? index = null;

        public int Index
        {
            get
            {
                if(index.HasValue)
                {
                    return index.Value;
                }

                if (int.TryParse(SectionName, out int sectionIndex) && sectionIndex >= 0)
                {
                    index = sectionIndex;
                }
                else
                {
                    index = int.MaxValue;
                }

                return index.Value;
            }
        }

        public ConfigurationEntry<string> Name = new ConfigurationEntry<string>("Player Name", "Player name of tribe member.");
    }
}
