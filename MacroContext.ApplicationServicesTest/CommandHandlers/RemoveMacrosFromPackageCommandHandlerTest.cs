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
using MacroContext.Contract.Events;

namespace MacroContext.ApplicationServicesTest.CommandHandlers
{
    [TestClass]
    public class RemoveMacrosFromPackageCommandHandlerTest
    {
        private RemoveMacrosFromPackageCommandHandler _handler;
        private RemoveMacrosFromPackageCommand _cmd;
        private IUnitOfWork _mockUow;
        private IMacroProfileRepository _mockRepo;
        private IEventStore _mockEventStore;
        private List<CompleteMacroDto> _completeMacroDtoCollection;



        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IMacroProfileRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockEventStore = Substitute.For<IEventStore>();
            _mockUow.MacroProfiles.Returns(_mockRepo);
            _handler = new RemoveMacrosFromPackageCommandHandler(_mockUow, _mockEventStore);
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



            _cmd = new RemoveMacrosFromPackageCommand(_completeMacroDtoCollection);
        }

        [TestMethod]
        public void ExecuteCommand_RepoRemovesProfiles()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().RemoveRange(Arg.Any<ICollection<MacroProfile>>());
        }



        [TestMethod]
        public void ExecuteCommand_NumberOfProfilesEqualsNumberOfDto()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().RemoveRange(Arg.Is<ICollection<MacroProfile>>(
                profileCollection => profileCollection.Count == _cmd.CompleteMacros.Count));
        }

        [TestMethod]
        public void ExecuteCommand_MacroRemovedEventsAreStored()
        {
            _handler.Execute(_cmd);
            _mockEventStore.Received(_completeMacroDtoCollection.Count).AddToEventQueue(Arg.Any<MacroRemovedEvent>());
        }


        [TestMethod]
        public void ExecuteCommand_ProfileAndDtoContainEqualProperties()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().RemoveRange(Arg.Is<ICollection<MacroProfile>>(
                profiles => ProfilesAndDtoHaveEqualProperties(profiles.ToArray(), _cmd.CompleteMacros.ToArray())
                ));
        }

        private bool ProfilesAndDtoHaveEqualProperties(MacroProfile[] profiles, CompleteMacroDto[] completeMacroDtoCollection)
        {
            for (var i = 0; i < completeMacroDtoCollection.Count(); i++)
            {
                var profile = profiles[i];
                var profileDto = completeMacroDtoCollection[i].MacroProfile;

                if (profile.Id != profileDto.Id) { return false; }
                else if (profile.RowVersion != null) { return false; }

            }
            return true;

        }

    }
}
