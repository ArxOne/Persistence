#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PersistentConfigurationAttribute : Attribute
    {
        public Type PersistentDataType { get; set; }
        public Type PersistentSerializerType { get; set; }
    }
}
