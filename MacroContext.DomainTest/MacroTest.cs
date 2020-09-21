using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Domain;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.DomainTest
{
    [TestClass]
    public class MacroTest
    {
        private Macro _macro;

        [TestInitialize]
        public void TestInitialize()
        {
            _macro = new Macro(Guid.NewGuid());

        }



        [TestMethod]
        public void EditInformation_NameEqualsInput()
        {
            var name = " myName";
            _macro.EditInfo(name,null, null, MacroType.Declaration, null);
            Assert.AreEqual(name, _macro.Name);

        }


        [TestMethod]
        public void EditInformation_DescriptionEqualsInput()
        {
            var description = "myDescription";
            _macro.EditInfo(null, description, null, MacroType.Declaration, null);
            Assert.AreEqual(description, _macro.Description);

        }

        [TestMethod]
        public void EditInformation_CodeEqualsInput()
        {
            var code = "fake";
            _macro.EditInfo(null, null, code, MacroType.Declaration, null);
            Assert.AreEqual(code, _macro.Code);

        }

        [TestMethod]
        public void EditInformation_MacroTypeEqualsInput()
        {
            var type = MacroType.Declaration;
            _macro.EditInfo(null, null, null, MacroType.Declaration, null);
            Assert.AreEqual(type, _macro.MacroType);

        }

        [TestMethod]
        public void EditInformation_RowVersionEqualsInput()
        {
            var version = new byte[0];
            _macro.EditInfo(null, null, null, MacroType.Declaration, version);
            Assert.AreEqual(version, _macro.RowVersion);

        }
    }
}
