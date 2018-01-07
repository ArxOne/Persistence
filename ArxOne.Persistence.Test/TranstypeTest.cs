#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Reflection;

    [TestClass]
    public class TranstypeTest
    {
        public enum E
        {
            A = 1,
            B = 2,
            C = 3,
        }

        [TestMethod] public void StringToIntTest() => Assert.AreEqual(12, Transtyper.Transtype<int>("12"));
        [TestMethod] public void IntToStringTest() => Assert.AreEqual("34", Transtyper.Transtype<string>(34));
        [TestMethod] public void StringToBoolTest() => Assert.AreEqual(true, Transtyper.Transtype<bool>("true"));
        [TestMethod] public void StringToBool2Test() => Assert.AreEqual(false, Transtyper.Transtype<bool>("false"));
        [TestMethod] public void BoolToStringTest() => Assert.AreEqual("False", Transtyper.Transtype<string>(false));
        [TestMethod] public void StringToVersionTest() => Assert.AreEqual(new Version(5, 6), Transtyper.Transtype<Version>("5.6"));
        [TestMethod] public void VersionToStringTest() => Assert.AreEqual("7.8", Transtyper.Transtype<string>(new Version(7, 8)));
        [TestMethod] public void StringToEnumTest() => Assert.AreEqual(E.B, Transtyper.Transtype<E>("B"));
        [TestMethod] public void EnumToStringTest() => Assert.AreEqual("C", Transtyper.Transtype<string>(E.C));
        [TestMethod] public void IntToEnumTest() => Assert.AreEqual(E.A, Transtyper.Transtype<E>(1));
        [TestMethod] public void EnumToIntTest() => Assert.AreEqual(3, Transtyper.Transtype<int>(E.C));
        [TestMethod] public void IntToBoolTest() => Assert.AreEqual(1, Transtyper.Transtype<int>(true));
        [TestMethod] public void BoolToIntTest() => Assert.AreEqual(true, Transtyper.Transtype<bool>(1));
    }
}