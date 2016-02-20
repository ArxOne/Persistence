#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Data;
    using Serializer;

    internal static class Configuration
    {
        private class AssemblyConfiguration
        {
            public IPersistentData Data;
            public IPersistentSerializer Serializer;
        };

        private static readonly IDictionary<AssemblyName, AssemblyConfiguration> ConfigurationByAssembly = new Dictionary<AssemblyName, AssemblyConfiguration>();
        private static readonly IDictionary<PropertyInfo, AssemblyConfiguration> ConfigurationByProperty = new Dictionary<PropertyInfo, AssemblyConfiguration>();
        private static readonly IDictionary<Type, object> Instances = new Dictionary<Type, object>();

        public static IPersistentSerializer GetSerializer(PropertyInfo propertyInfo)
        {
            return GetConfiguration(propertyInfo).Serializer;
        }

        public static IPersistentData GetData(PropertyInfo propertyInfo)
        {
            return GetConfiguration(propertyInfo).Data;
        }

        /// <summary>
        /// Gets the configuration, given a property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns></returns>
        private static AssemblyConfiguration GetConfiguration(PropertyInfo propertyInfo)
        {
            lock (ConfigurationByProperty)
            {
                AssemblyConfiguration configuration;
                if (!ConfigurationByProperty.TryGetValue(propertyInfo, out configuration))
                {
                    configuration = GetConfiguration(propertyInfo.DeclaringType.Assembly);
                    ConfigurationByProperty[propertyInfo] = configuration;
                }
                return configuration;
            }
        }

        /// <summary>
        /// Gets the configuration, given an assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        private static AssemblyConfiguration GetConfiguration(Assembly assembly)
        {
            lock (ConfigurationByAssembly)
            {
                AssemblyConfiguration configuration;
                var assemblyName = assembly.GetName();
                if (!ConfigurationByAssembly.TryGetValue(assemblyName, out configuration))
                {
                    var configurationAttribute = assembly.GetCustomAttributes(typeof(PersistentConfigurationAttribute), false)
                        .OfType<PersistentConfigurationAttribute>().SingleOrDefault();
                    var persistentDataType = configurationAttribute?.PersistentDataType ?? typeof(PersistentData);
                    var persistentSerializerType = configurationAttribute?.PersistentSerializerType ?? typeof(RegistryPersistentSerializer);
                    configuration = new AssemblyConfiguration
                    {
                        Data = (IPersistentData)GetInstance(persistentDataType),
                        Serializer = (IPersistentSerializer)GetInstance(persistentSerializerType)
                    };
                    ConfigurationByAssembly[assemblyName] = configuration;
                }
                return configuration;
            }
        }

        /// <summary>
        /// Gets the instance of a given type (uses singletons).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static object GetInstance(Type type)
        {
            lock (Instances)
            {
                object instance;
                if (!Instances.TryGetValue(type, out instance))
                {
                    instance = Activator.CreateInstance(type);
                    Instances[type] = instance;
                }
                return instance;
            }
        }

        /// <summary>
        /// Writes all unflushed data.
        /// </summary>
        internal static void Write()
        {
            foreach (var assemblyConfiguration in ConfigurationByAssembly)
            {
                assemblyConfiguration.Value.Data.Write(assemblyConfiguration.Value.Serializer);
            }
        }
    }
}
