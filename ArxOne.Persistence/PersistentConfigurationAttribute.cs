#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence
{
    using System;
    using Data;
    using Serializer;

    /// <summary>
    /// Assembly-level configuration for persistence.
    /// This is optional, as is every member, in which case a default implementation is used
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PersistentConfigurationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the persistent data.
        /// The type must implement <see cref="IPersistentData"/>
        /// </summary>
        /// <value>
        /// The type of the persistent data.
        /// </value>
        public Type PersistentDataType { get; set; }
        /// <summary>
        /// Gets or sets the type of the persistent serializer.
        /// The type must implement <see cref="IPersistentSerializer"/>
        /// </summary>
        /// <value>
        /// The type of the persistent serializer.
        /// </value>
        public Type PersistentSerializerType { get; set; }
    }
}
