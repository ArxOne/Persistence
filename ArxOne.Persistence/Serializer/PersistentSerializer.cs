#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Serializer
{
    using System;
    using Reflection;

    /// <summary>
    /// Base class for persistent serializers implemented <see cref="IPersistentSerializer"/>
    /// </summary>
    /// <seealso cref="ArxOne.Persistence.Serializer.IPersistentSerializer" />
    public abstract class PersistentSerializer : IPersistentSerializer
    {
        /// <summary>
        /// Tries to load the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryLoadValue(string name, Type valueType, out object value)
        {
            if (!ReadValue(name, out var rawValue))
            {
                value = null;
                return false;
            }

            value = Transtyper.FromPersistent(rawValue, valueType);
            return true;
        }

        /// <summary>
        /// Saves the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        public void SaveValue(string name, Type valueType, object value)
        {
            var persistedValue = Transtyper.ToPersistent(value, Mappings, out var tag);
            WriteValue(name, persistedValue, tag);
        }

        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        protected abstract TypeMappings Mappings { get; }

        /// <summary>
        /// Reads the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected abstract bool ReadValue(string name, out object value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="tag">The tag.</param>
        protected abstract void WriteValue(string name, object value, object tag);
    }
}