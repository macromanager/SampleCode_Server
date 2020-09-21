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
    public class EditPackageCommandHandlerTest
    {
        private EditPackageCommandHandler _handler;
        private EditPackageCommand _cmd;
        private PackageDto _packageDto;
        private ICommand[] _macroCommands;
        private ICommand[] _refCommands;


        private IUnitOfWork _mockUow;
        private IPackageRepository _mockRepo;
        private IEventStore _mockEventStore;
        private ICommandDispatcher _mockCommandDispatcher;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IPackageRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.Packages.Returns(_mockRepo);
            _mockEventStore = Substitute.For<IEventStore>();
            _mockCommandDispatcher = Substitute.For<ICommandDispatcher>();
            
            _handler = new EditPackageCommandHandler(_mockUow, _mockEventStore, _mockCommandDispatcher);

            _packageDto = new PackageDto()
            {
                Id = Guid.NewGuid(),
                Name = "myName",
                Description = "myDescription",

            };

            _macroCommands = new ICommand[0];
            _refCommands = new ICommand[0];

            _cmd = new EditPackageCommand(_packageDto, _macroCommands, _refCommands);
        }

        [TestMethod]
        public void ExecuteCommand_WithValidInputs()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().Update(Arg.Is<Package>(
                pkg =>
                IsPackageDataCorrect(pkg, _cmd.Package)
                ));
        }


        [TestMethod]
        public void ExecuteCommand_EventAddedToStore()
        {
            _handler.Execute(_cmd);
            _mockEventStore.Received().AddToEventQueue(Arg.Is<PackageEditedEvent>(e => e.PackageId == _cmd.Package.Id));

        }

        private bool IsPackageDataCorrect(Package package, PackageDto packageDto)
        {
            var isCorrect = true;
            if(package.Id != packageDto.Id) { isCorrect = false; }
            else if(package.Name != packageDto.Name) { isCorrect = false; }
            else if(package.Description != packageDto.Description) { isCorrect = false; }
            return isCorrect;
        }

    }
}
