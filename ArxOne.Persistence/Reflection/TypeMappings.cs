#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Represents a bunch of <see cref="TypeMapping" />
    /// </summary>
    public class TypeMappings
    {
        private readonly Dictionary<Type, TypeMapping> _mappings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeMappings" /> class.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public TypeMappings(params TypeMapping[] mappings)
        {
            _mappings = mappings.ToDictionary(m => m.Clr, m => m);
        }

        /// <summary>
        /// Gets the persisted type.
        /// </summary>
        /// <param name="clrType">Type of the color.</param>
        /// <returns></returns>
        public TypeMapping GetPersisted(Type clrType)
        {
            _mappings.TryGetValue(clrType, out var persistedType);
            return persistedType;
        }
    }
}
