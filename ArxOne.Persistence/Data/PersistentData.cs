#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Data
{
    using System;
    using System.Collections.Generic;
    using Serializer;

    public class PersistentData : IPersistentData
    {
        private class PersistentValue
        {
            /// <summary>
            /// The name under which the value is persisted
            /// </summary>
            public string Name;
            /// <summary>
            /// The actual value
            /// </summary>
            public object Value;
            /// <summary>
            /// The value type
            /// </summary>
            public Type ValueType;
            /// <summary>
            /// true is value is modified but not persisted
            /// </summary>
            public bool Dirty;
        }

        private readonly IDictionary<string, PersistentValue> _persistentValues = new Dictionary<string, PersistentValue>();

        /// <summary>
        /// Writes all changes down to persistence.
        /// </summary>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        public void Write(IPersistentSerializer persistentSerializer)
        {
            lock (_persistentValues)
            {
                foreach (var persistentValue in _persistentValues.Values)
                {
                    if (persistentValue.Dirty)
                        Write(persistentValue, persistentSerializer);
                }
            }
        }

        /// <summary>
        /// Writes the specified persistent value.
        /// </summary>
        /// <param name="persistentValue">The persistent value.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        private static void Write(PersistentValue persistentValue, IPersistentSerializer persistentSerializer)
        {
            persistentSerializer.SaveValue(persistentValue.Name, persistentValue.ValueType, persistentValue.Value);
            persistentValue.Dirty = false;
        }

        /// <summary>
        /// Gets a value, or default value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType"></param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        /// <returns></returns>
        public object GetValue(string name, Type valueType, object defaultValue, IPersistentSerializer persistentSerializer)
        {
            return GetPersistentValue(name, valueType, defaultValue, persistentSerializer).Value;
        }

        /// <summary>
        /// Gets the persistent value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        /// <returns></returns>
        private PersistentValue GetPersistentValue(string name, Type valueType, object defaultValue, IPersistentSerializer persistentSerializer)
        {
            lock (_persistentValues)
            {
                PersistentValue persistentValue;
                if (!_persistentValues.TryGetValue(name, out persistentValue))
                {
                    // value is read from serializer only if not found in memory
                    object value;
                    if (!persistentSerializer.TryLoadValue(name, valueType, out value))
                        value = defaultValue;
                    persistentValue = new PersistentValue
                    {
                        Name = name,
                        Value = value,
                        ValueType = valueType,
                        Dirty = false,
                    };
                }
                return persistentValue;
            }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="valueType"></param>
        /// <param name="writeNow">if set to <c>true</c> persists the value immediately.</param>
        /// <param name="persistentSerializer">The persistent serializer.</param>
        public void SetValue(string name, object value, Type valueType, bool writeNow, IPersistentSerializer persistentSerializer)
        {
            lock (_persistentValues)
            {
                PersistentValue persistentValue;
                // if the value is not found (this is probably rare), then a container is created
                if (!_persistentValues.TryGetValue(name, out persistentValue))
                {
                    persistentValue = new PersistentValue
                    {
                        Name = name,
                        ValueType = valueType,
                    };
                    _persistentValues[name] = persistentValue;
                }
                // then the value is set anyway
                // TODO: conditional dirty (if value actually changes)
                persistentValue.Value = value;
                persistentValue.Dirty = true;
                // if it has to be written immediately, then do it
                if (writeNow)
                    Write(persistentValue, persistentSerializer);
            }
        }
    }
}