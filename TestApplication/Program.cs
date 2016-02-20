#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

using ArxOne.Persistence.Serializer;

[assembly: RegistryPersistence("ArxOne.Persistence.Test")]

namespace TestApplication
{
    using ArxOne.Persistence;

    public static class Program
    {
        [Persistent("IntValue", DefaultValue = 0)]
        public static int IntValue { get; set; }

        public static void Main(string[] args)
        {
            ++IntValue;
        }
    }
}
