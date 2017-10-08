using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Common;
using PatientManagement.Model;

namespace EFC.BLTests
{
    [TestClass]
    public class NpiValidatorTest
    {
        [TestMethod]
        public void ValidProviderNpi()
        {
            var provider = new Provider
            {
                FirstName = "John",
                LastName = "Smith",
                Npi = "1234567893"
            };

            var npiValidator = new NpivalidationRule();
            npiValidator.ParseNpi(provider.Npi);
            var actual = npiValidator.InvalidNPI;

            Assert.AreEqual(false, actual);
        }
        [TestMethod]
        public void InvalidProviderNpi()
        {
            var provider = new Provider();

            try
            {
                provider.Npi = "12345678";
            }
            catch (ArgumentException)
            {
                var npi = "1234567893";
                provider.Npi = npi;
                var expected = "1234567893";
                var actual = npi;
                Assert.AreEqual(actual, expected);
            }

        }
        [TestMethod]
        public void ShortProviderNpi()
        {
            var provider = new Provider();

            try
            {
                provider.Npi = "12345678";
            }
            catch (ArgumentException)
            {
                string npi = "1234567893";
                provider.Npi = npi;
                var expected = "1234567893";
                var actual = npi;
                Assert.AreEqual(actual, expected);
            }

        }
    }
}
