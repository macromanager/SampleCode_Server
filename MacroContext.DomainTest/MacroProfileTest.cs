using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Domain;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.DomainTest
{
    [TestClass]
    public class MacroProfileTest
    {
        private MacroProfile _profile;


        [TestInitialize]
        public void TestInitialize()
        {
            _profile = new MacroProfile(Guid.NewGuid(),Guid.NewGuid(), new Macro(Guid.NewGuid()));

        }


        [TestMethod]
        public void UpdateProfile_MacroPositionEqualsInput()
        {
            var macroPosition = 1;
            _profile.UpdateProfile(macroPosition, ComponentType.ClassModule, null, null);
            Assert.AreEqual(macroPosition, _profile.MacroPosition);
        }

        [TestMethod]
        public void UpdateProfile_ComponentTypeEqualsInput()
        {
            var type = ComponentType.ClassModule;
            _profile.UpdateProfile(0, type, null, null);
            Assert.AreEqual(type, _profile.ComponentType);

        }


        [TestMethod]
        public void UpdateProfile_ComponentNameEqualsInput()
        {
            var modName = "myModule";
            _profile.UpdateProfile(0, ComponentType.ClassModule, modName, null);
            Assert.AreEqual(modName, _profile.ComponentName);

        }

        [TestMethod]
        public void UpdateProfile_RowVersionEqualsInput()
        {
            var version = new byte[0];
            _profile.UpdateProfile(0, ComponentType.ClassModule, null, version);
            Assert.AreEqual(version, _profile.RowVersion);

        }


    }
}
