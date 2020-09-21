using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Domain;

namespace MacroContext.DomainTest
{
    [TestClass]
    public class PackageTest
    {
        private Package _package;

        [TestInitialize]
        public void TestInitialize()
        {
            _package = new Package(Guid.NewGuid());

        }
        public void EditInformation_NameEqualsInput()
        {
            var name = " myName";
            _package.EditInfo(Guid.NewGuid(), name, null, null);
            Assert.AreEqual(name, _package.Name);

        }


        [TestMethod]
        public void EditInformation_DescriptionEqualsInput()
        {
            var description = "myDescription";
            _package.EditInfo(Guid.NewGuid(), null, description, null);
            Assert.AreEqual(description, _package.Description);

        }

        [TestMethod]
        public void EditInformation_RowVersionEqualsInput()
        {
            var version = new byte[0];
            _package.EditInfo(Guid.NewGuid(), null, null, version);
            Assert.AreEqual(version, _package.RowVersion);

        }

    }
}
