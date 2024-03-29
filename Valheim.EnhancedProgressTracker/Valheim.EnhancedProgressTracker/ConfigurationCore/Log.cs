﻿using BepInEx.Logging;
using System;
using UnityEngine;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker.ConfigurationCore
{
    internal static class Log
    {
        internal static ManualLogSource Logger;

        public static void LogDebug(string message)
        {
            if (ConfigurationManager.GeneralConfig?.DebugLoggingOn?.Value == true)
            {
                Logger.LogInfo($"{message}");
            }
        }

        public static void LogTrace(string message)
        {
            if (ConfigurationManager.GeneralConfig?.TraceLoggingOn?.Value == true)
            {
                Logger.LogDebug($"{message}");
            }
        }

        public static void LogInfo(string message) => Logger.LogMessage($"{message}");

        public static void LogWarning(string message) => Logger.LogWarning($"{message}");

        public static void LogError(string message, Exception e = null) => Logger.LogError($"{message}. {e?.Message}");
    }
}
