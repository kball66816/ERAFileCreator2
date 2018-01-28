using Common.Common.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.CommonTests
{
    [TestClass]
    public class NpiValidatiorRuleTest
    {
        [TestMethod]
        public void ValidProviderNpi()
        {
            var npi = "1234567893";
            var validator = new NpivalidationRule();
            validator.ParseNpi(npi);
            var actual = validator.InvalidNpi;
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void FailValidProviderNpi()
        {
            var npi = "1234567893";
            var validator = new NpivalidationRule();
            validator.ParseNpi(npi);
            var actual = validator.InvalidNpi;
            Assert.AreNotEqual(true, actual);
        }

        [TestMethod]
        public void InvalidProviderNpi()
        {
            var npi = "1234567894";
            var validator = new NpivalidationRule();
            validator.ParseNpi(npi);
            var actual = validator.InvalidNpi;
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void ShortProviderNpi()
        {
            var npi = "12345678";
            var validator = new NpivalidationRule();
            validator.ParseNpi(npi);
            var actual = validator.InvalidNpi;
            Assert.AreEqual(true, actual);
        }
    }
}