#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion
namespace ArxOne.Persistence.Serializer
{
    using System;

    public class RegistryPersistentSerializer : IPersistentSerializer
    {
        public bool TryLoadValue(string name, Type valueType, out object value)
        {
            throw new NotImplementedException();
        }

        public void SaveValue(string name, Type valueType, object value)
        {
            throw new NotImplementedException();
        }
    }
}