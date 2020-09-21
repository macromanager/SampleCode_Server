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
    public class EditMacrosCommandHandlerTest
    {
        private EditMacrosCommandHandler _handler;
        private EditMacrosCommand _cmd;
        private IUnitOfWork _mockUow;
        private IMacroRepository _mockMacroRepo;
        private IMacroProfileRepository _mockProfileRepo;
        private IEventStore _mockEventStore;
        private List<CompleteMacroDto> _completeMacroDtoCollection;



        [TestInitialize]
        public void TestInitialize()
        {
            _mockMacroRepo = Substitute.For<IMacroRepository>();
            _mockProfileRepo = Substitute.For<IMacroProfileRepository>();

            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.Macros.Returns(_mockMacroRepo);
            _mockUow.MacroProfiles.Returns(_mockProfileRepo);
            _mockEventStore = Substitute.For<IEventStore>();
            _handler = new EditMacrosCommandHandler(_mockUow, _mockEventStore);
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



            _cmd = new EditMacrosCommand(_completeMacroDtoCollection);
        }

        [TestMethod]
        public void ExecuteCommand_RepoEditedMacros()
        {
            _handler.Execute(_cmd);
            _mockMacroRepo.Received(_completeMacroDtoCollection.Count).Update(Arg.Any<Macro>());
        }


        [TestMethod]
        public void ExecuteCommand_RepoEditedProfiles()
        {
            _handler.Execute(_cmd);
            _mockProfileRepo.Received(_completeMacroDtoCollection.Count).Update(Arg.Any<MacroProfile>());
        }



    }
}
