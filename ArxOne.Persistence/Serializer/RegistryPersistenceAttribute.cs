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
        /// The registry node
        /// </summary>
        public static string RegistryNode;

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>
        /// The node.
        /// </value>
        public string Node
        {
            get { return RegistryNode; }
            set { RegistryNode = value; }
        }

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
