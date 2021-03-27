
namespace Valheim.EnhancedProgressTracker.GlobalKey
{
    public enum KeyMode
    {
        /// <summary>
        /// Default tracking conditions. Will simply add in enhanced tracking kill progress.
        /// </summary>
        Default,

        /// <summary>
        /// Tracks progress by player. Each player will have his own kill progress.
        /// </summary>
        Player,

        /// <summary>
        /// Tracks progress by tribe. Each tribe of players will have their own kill progress.
        /// </summary>
        Tribe
    }
}
