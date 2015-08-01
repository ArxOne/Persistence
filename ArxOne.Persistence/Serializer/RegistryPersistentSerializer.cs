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
    /// Registry persistent serializer. This is the default implementation
    /// </summary>
    public class RegistryPersistentSerializer : IPersistentSerializer
    {
        bool IPersistentSerializer.TryLoadValue(string name, Type valueType, out object value)
        {
            throw new NotImplementedException();
        }

        void IPersistentSerializer.SaveValue(string name, Type valueType, object value)
        {
            throw new NotImplementedException();
        }
    }
}