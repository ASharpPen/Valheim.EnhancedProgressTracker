using BepInEx;
using HarmonyLib;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker
{
    [BepInPlugin("asharppen.valheim.enhanced_progress_tracker", "EnhancedProgressTracker", "1.0.0")]
    public class EnhancedProgressTracker : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            Log.Logger = Logger;

            ConfigurationManager.LoadAllConfigurations();

            new Harmony("mod.enhanced_progress_tracker").PatchAll();
        }
    }
}
