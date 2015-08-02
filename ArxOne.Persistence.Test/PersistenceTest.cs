#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

using ArxOne.Persistence.Serializer;

[assembly: RegistryPersistence("ArxOne.Persistence.Test")]

namespace ArxOne.Persistence.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PersistenceTest
    {
        public class DefaultValue
        {
            [Persistent("A", DefaultValue = "nope")]
            public string A { get; set; }
        }

        public class Share1
        {
            [Persistent("ShareA")]
            public string A { get; set; }
        }

        public class Share2
        {
            [Persistent("ShareA")]
            public string B { get; set; }
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
    }
}
