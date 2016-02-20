#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence
{
    /// <summary>
    /// Global class
    /// </summary>
    public static class Persistence
    {
        /// <summary>
        /// Writes all dirty data.
        /// </summary>
        public static void Write() => Configuration.Write();
    }
}