#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PersistenceTest
    {
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

        }
    }
}
