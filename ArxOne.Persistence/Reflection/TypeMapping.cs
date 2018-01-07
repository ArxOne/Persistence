#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Reflection
{
    using System;

    /// <summary>
    ///     Represents a mapping between CLR (the application) and persistence
    /// </summary>
    public class TypeMapping
    {
        /// <summary>
        ///     The CLR type.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public Type Clr { get; }

        /// <summary>
        ///     Gets the persisted type.
        /// </summary>
        /// <value>
        ///     The persisted.
        /// </value>
        public Type Persisted { get; }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public object Tag { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMapping" /> class.
        /// </summary>
        /// <param name="clr">The color.</param>
        /// <param name="persisted">The persisted.</param>
        /// <param name="tag">The tag.</param>
        public TypeMapping(Type clr, Type persisted, object tag = null)
        {
            Clr = clr;
            Persisted = persisted;
            Tag = tag;
        }

        /// <summary>
        /// Creates a <see cref="TypeMapping" />.
        /// </summary>
        /// <typeparam name="TClr">The type of the color.</typeparam>
        /// <typeparam name="TPersisted">The type of the persisted.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public static TypeMapping Create<TClr, TPersisted>(object tag = null)
        {
            return new TypeMapping(typeof(TClr), typeof(TPersisted), tag);
        }
    }
}
