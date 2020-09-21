using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Dto;
using MacroContext.Contract.Commands;
using MacroContext.Persistance;
using NSubstitute;
using System.Collections.Generic;
using MacroContext.Domain;
using ApplicationServices.CommandHandlers;

namespace MacroContext.ApplicationServicesTest.CommandHandlers
{
    [TestClass]
    public class AddMacrosToPackageCommandHandlerTest
    {
        private AddMacrosToPackageCommandHandler _handler;
        private AddMacrosToPackageCommand _cmd;
        private IUnitOfWork _mockUow;
        private IMacroProfileRepository _mockRepo;
        private List<CompleteMacroDto> _completeMacroDtoCollection;
        private IEventStore _eventStore;



        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IMacroProfileRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.MacroProfiles.Returns(_mockRepo);
            _eventStore = Substitute.For<IEventStore>();
            _handler = new AddMacrosToPackageCommandHandler(_mockUow, _eventStore);
            var packageId = Guid.NewGuid();
            _completeMacroDtoCollection = new List<CompleteMacroDto>();
            for(var i = 0; i < 5; i++)
            {
                var macroDto = new MacroDto(Guid.NewGuid())
                {
                    Description = "testDescription" + i,
                    Name = "testName" + i,
                };

                var profileDto = new MacroProfileDto(Guid.NewGuid(), packageId, macroDto.Id)
                {
                    ComponentName = "testModule" + i,
                    MacroPosition = i,
                };


                
                var completeMacro = new CompleteMacroDto(macroDto, profileDto);
                _completeMacroDtoCollection.Add(completeMacro);

            }



            _cmd = new AddMacrosToPackageCommand(_completeMacroDtoCollection);
        }

        [TestMethod]
        public void ExecuteCommand_RepoAddedProfiles()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().AddRange(Arg.Any<ICollection<MacroProfile>>());
        }



        [TestMethod]
        public void ExecuteCommand_NumberOfProfilesEqualsNumberOfDto()
        {
            _handler.Execute(_cmd);

            _mockRepo.Received().AddRange(Arg.Is<ICollection<MacroProfile>>(
                profileCollection => profileCollection.Count == _cmd.CompleteMacros.Count));
        }

        [TestMethod]
        public void ExecuteCommand_ProfileAndDtoContainEqualProperties()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().AddRange(Arg.Is<ICollection<MacroProfile>>(
                profiles => ProfilesAndDtoHaveEqualProperties(profiles.ToArray(), _cmd.CompleteMacros.ToArray())
                ));
        }


        [TestMethod]
        public void ExecuteCommand_MacroAndDtoContainEqualProperties()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().AddRange(Arg.Is<ICollection<MacroProfile>>(
                profiles => MacrosAndDtoHaveEqualProperties(
                    profiles.Select(p => p.Macro).ToArray(),
                    _cmd.CompleteMacros.Select(cmd => cmd.Macro).ToArray())
                ));
        }


        private bool ProfilesAndDtoHaveEqualProperties(MacroProfile[] profiles, CompleteMacroDto[] completeMacroDtoCollection)
        {
            for (var i = 0; i < completeMacroDtoCollection.Count(); i++)
            {
                var profile = profiles[i];
                var profileDto = completeMacroDtoCollection[i].MacroProfile;

                if (profile.MacroPosition != profileDto.MacroPosition) { return false; }
                else if (profile.Id != profileDto.Id) { return false; }
                else if (profile.ComponentName != profileDto.ComponentName) { return false; }
                else if (profile.PackageId != profileDto.PackageId) { return false; }
                else if (profile.RowVersion != null) { return false; }

            }
            return true;

        }

        private bool MacrosAndDtoHaveEqualProperties(Macro[] macros, MacroDto[] macroDtoCollection)
        {
            var pass = true;
            for (var i = 0; i < macros.Count(); i++)
            {
                var macro = macros[i];
                var macroDto = macroDtoCollection[i];
                if (macro.Name != macroDto.Name) { pass = false; }
                else if (macro.Description != macroDto.Description) { pass = false; }
                else if (macro.Id != macroDto.Id) { pass = false; }
                else if (macro.RowVersion != null) { pass = false; }


            }

            return pass;
        }

    }
}
