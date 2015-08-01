#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Serializer
{
    using System;

    /// <summary>
    /// Persistent serializer interface
    /// </summary>
    public interface IPersistentSerializer
    {
        /// <summary>
        /// Tries to load the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool TryLoadValue(string name, Type valueType, out object value);

        /// <summary>
        /// Saves the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        void SaveValue(string name, Type valueType, object value);
    }
}
