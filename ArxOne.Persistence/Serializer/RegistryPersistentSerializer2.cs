#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

using ArxOne.Persistence.Reflection;

namespace ArxOne.Persistence.Serializer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Win32;

    /// <summary>
    /// New registry <see cref="IPersistentSerializer"/> implementation
    /// </summary>
    /// <seealso cref="ArxOne.Persistence.Serializer.PersistentSerializer" />
    public class RegistryPersistentSerializer2 : PersistentSerializer
    {
        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        protected override TypeMappings Mappings { get; }
            = new TypeMappings(
                TypeMapping.Create<string, string>(RegistryValueKind.String),
                TypeMapping.Create<Uri, string>(RegistryValueKind.String),
                TypeMapping.Create<Version, string>(RegistryValueKind.String),
                TypeMapping.Create<Enum, string>(RegistryValueKind.String),
                TypeMapping.Create<int, int>(RegistryValueKind.DWord),
                TypeMapping.Create<long, long>(RegistryValueKind.QWord),
                TypeMapping.Create<bool, int>(RegistryValueKind.DWord),
                TypeMapping.Create<byte[], byte[]>(RegistryValueKind.Binary)
                );

        private RegistryKey RegistryKey { get; }
        private string RegistryNode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryPersistentSerializer"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public RegistryPersistentSerializer2(Assembly assembly)
        {
            var registryPersistenceAttribute = assembly.GetCustomAttributes(typeof(RegistryPersistenceAttribute), false)
                .OfType<RegistryPersistenceAttribute>().FirstOrDefault();
            if (registryPersistenceAttribute != null)
            {
                RegistryKey = registryPersistenceAttribute.CurrentUser ? Registry.CurrentUser : Registry.LocalMachine;
                RegistryNode = registryPersistenceAttribute.Node;
            }
            else
            {
                RegistryKey = Registry.CurrentUser;
                RegistryNode = assembly.GetName().Name;
            }
        }

        private RegistryKey CreateSubKey()
        {
            return RegistryKey.CreateSubKey(@"Software\" + RegistryNode);
        }

        /// <summary>
        /// Reads the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override bool ReadValue(string name, out object value)
        {
            using (var r = CreateSubKey())
            {
                if (!r.GetValueNames().Contains(name))
                {
                    value = null;
                    return false;
                }

                if (r.GetValueKind(name) == RegistryValueKind.None)
                    value = null;
                else
                    value = r.GetValue(name);
                return true;
            }
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="tag">The tag.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteValue(string name, object value, object tag)
        {
            using (var r = CreateSubKey())
            {
                if (value == null)
                    r.SetValue(name, new byte[0], RegistryValueKind.None);
                else
                    r.SetValue(name, value, (RegistryValueKind)tag);
            }
        }
    }
}
