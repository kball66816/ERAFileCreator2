using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFC.BL;

namespace EFC.BLTests
{
    [TestClass]
    public class AddonTest
    {
        [TestMethod]
        public void TestClone()
        {
            AddonCharge addon = new AddonCharge()
            {
                ChargeCost = 50
            };
            AddonCharge addon2 = (AddonCharge)addon.Clone();

            Console.WriteLine(addon2.ChargeCost.ToString());


            addon2.ChargeCost = 25;

            var expected = 50;
            var actual = addon.ChargeCost;

            Console.WriteLine(addon.ChargeCost.ToString());
            Console.WriteLine(addon2.ChargeCost.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}
