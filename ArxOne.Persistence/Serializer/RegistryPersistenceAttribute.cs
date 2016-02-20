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
    /// Sets the registry root (otherwise, default value based on assembly name is used)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class RegistryPersistenceAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>
        /// The node.
        /// </value>
        public string Node { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether data should be serialized to current user (defaults to true).
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current user]; otherwise, <c>false</c>.
        /// </value>
        public bool CurrentUser { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryPersistenceAttribute"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public RegistryPersistenceAttribute(string node)
        {
            Node = node;
        }
    }
}
