#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion
namespace ArxOne.Persistence.Serializer
{
    using System;
    using Microsoft.Win32;
    using Utility;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Registry persistent serializer. This is the default implementation
    /// </summary>
    public class RegistryPersistentSerializer : IPersistentSerializer
    {
        private RegistryKey RegistryKey { get; }
        private string RegistryNode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryPersistentSerializer"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public RegistryPersistentSerializer(Assembly assembly)
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

        bool IPersistentSerializer.TryLoadValue(string name, Type valueType, out object value)
        {
            using (var r = CreateSubKey())
            {
                if (!r.GetValueNames().Contains(name))
                {
                    value = null;
                    return false;
                }
                value = ReadValue(r, name);
                // some basic transtyping here
                if (valueType.IsEnum)
                    value = Enum.Parse(valueType, (string)value);
                return true;
            }
        }
        private static object ReadValue(RegistryKey r, string n)
        {
            if (r.GetValueKind(n) == RegistryValueKind.None)
                return null;
            return r.GetValue(n);
        }

        void IPersistentSerializer.SaveValue(string name, Type valueType, object value)
        {
            using (var r = CreateSubKey())
            {
                var qualifiedValue = GetValue(value);
                r.SetValue(name, qualifiedValue.Item1 ?? new byte[0], qualifiedValue.Item2);
            }
        }

        /// <summary>
        /// Gets a pair value/registry kind.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        private static Tuple<object, RegistryValueKind> GetValue(object o)
        {
            if (o == null)
                return Tuple.Create<object, RegistryValueKind>(null, RegistryValueKind.None);
            var t = o.GetType();
            return GetValue(o, t);
        }

        /// <summary>
        /// Gets a pair value/registry kind.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">@Unsupported type;t</exception>
        private static Tuple<object, RegistryValueKind> GetValue(object o, Type t)
        {
            if (t.IsNullable())
                return GetValue(o, t.GetNullabled());

            if (t == typeof(string))
                return Tuple.Create(o, RegistryValueKind.String);
            if (t == typeof(Uri))
                return Tuple.Create(o, RegistryValueKind.String);

            if (t.IsEnum)
                return Tuple.Create(o, RegistryValueKind.String);

            if (t == typeof(int))
                return Tuple.Create(o, RegistryValueKind.DWord);

            if (t == typeof(long))
                return Tuple.Create(o, RegistryValueKind.QWord);

            if (t == typeof(bool))
                return Tuple.Create(o, RegistryValueKind.DWord);

            if (t == typeof(byte[]))
                return Tuple.Create(o, RegistryValueKind.Binary);

            throw new ArgumentException(@"Unsupported type", nameof(t));
        }
    }
}