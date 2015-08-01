#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Data
{
    using System;
    using Serializer;

    public interface IPersistentData
    {
        /// <summary>
        /// Writes all changes down to persistence.
        /// </summary>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        void Write(IPersistentSerializer persistentSerializer);

        /// <summary>
        /// Gets a value, or default value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType"></param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        /// <returns></returns>
        object GetValue(string name, Type valueType, object defaultValue, IPersistentSerializer persistentSerializer);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="valueType"></param>
        /// <param name="writeNow">if set to <c>true</c> persists the value immediately.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        void SetValue(string name, object value, Type valueType, bool writeNow, IPersistentSerializer persistentSerializer);
    }
}
