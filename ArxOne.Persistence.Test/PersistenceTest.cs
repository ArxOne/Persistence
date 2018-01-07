#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

using ArxOne.Persistence;
using ArxOne.Persistence.Serializer;

[assembly: RegistryPersistence("ArxOne.Persistence.Test")]
[assembly: PersistentConfiguration(PersistentSerializerType = typeof(RegistryPersistentSerializer2))]

namespace ArxOne.Persistence.Test
{
    using System;
    using System.Net.Mail;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MrAdvice.Advice;

    [TestClass]
    public class PersistenceTest
    {
        private Type _import = typeof(IAdvice);

        public class DefaultValue
        {
            [Persistent("A", DefaultValue = "nope")]
            public string A { get; set; }
        }

        public class Share1
        {
            [Persistent("ShareA")] public string A { get; set; }
        }

        public class Share2
        {
            [Persistent("ShareA")] public string B { get; set; }
        }

        [TestMethod]
        public void SimplePersistenceTest()
        {
            var s1 = new Share1();
            var s2 = new Share2();
            s1.A = Guid.NewGuid().ToString();
            Assert.AreEqual(s2.B, s1.A);
        }

        [TestMethod]
        public void DefaultValueTest()
        {
            var s1 = new DefaultValue();
            Assert.AreEqual("nope", s1.A);
        }

        public class C1
        {
            [Persistent("NB", AutoSave = true)] public bool? B { get; set; }
        }

        [TestMethod]
        public void NullableBoolTest()
        {
            var c1 = new C1();
            var b = c1.B;
            Assert.IsNull(b);
            c1.B = true;

            var c2 = new C1();
            Assert.AreEqual(true, c2.B);
            c2.B = null;

            var c3 = new C1();
            Assert.IsNull(c3.B);
        }

        public class Version1
        {
            [Persistent("Version")] public Version V { get; set; }
        }

        [TestMethod]
        public void VersionTest()
        {
            var v1 = new Version1();
            if (v1.V == null || v1.V.Major > 1000)
                v1.V = new Version(1, 0);
            else
                v1.V = new Version(v1.V.Major + 1, v1.V.Minor);
        }
    }
}
