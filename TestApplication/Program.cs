﻿#region Arx One Persistence

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
        public enum Z
        {
            A,
            B,
            C
        }

        [Persistent("IntValue", DefaultValue = 0)]
        public static int IntValue { get; set; }


        [Persistent("EnumValue", DefaultValue = Z.A)]
        public static Z EnumValue { get; set; }


        public static void Main(string[] args)
        {
            //++IntValue;
            if (EnumValue == Z.C)
                EnumValue = Z.A;
            else
                ++EnumValue;
        }
    }
}