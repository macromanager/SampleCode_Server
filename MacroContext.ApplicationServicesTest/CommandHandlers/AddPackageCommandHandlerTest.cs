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
    public class AddPackageCommandHandlerTest
    {
        private AddPackageCommandHandler _handler;
        private AddPackageCommand _cmd;
        private PackageDto _packageDto;
        private CompleteMacroDto[] _macros;
        private IUnitOfWork _mockUow;
        private IEventStore _mockEventStore;
        private IPackageRepository _mockRepo;
        private ICommandDispatcher _mockCommandDispatcher;
        
        //private ICollection<MacroDto> _macroDtoCollection;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IPackageRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockEventStore = Substitute.For<IEventStore>();
            _mockCommandDispatcher = Substitute.For<ICommandDispatcher>();
            _mockUow.Packages.Returns(_mockRepo);
            _handler = new AddPackageCommandHandler(_mockUow, _mockEventStore, _mockCommandDispatcher);
            //_macros = new CompleteMacroDto[0];
            _packageDto = new PackageDto()
            {
                Id = Guid.NewGuid(),
                Name = "myName",
                Description = "myDescription",

            };

            _cmd = new AddPackageCommand(_packageDto, null, null);
        }

        [TestMethod]
        public void ExecuteCommand_RepoAddsData()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().Add(Arg.Is<Package>(
                pkg =>
                IsPackageDataCorrect(pkg, _cmd.Package)
                ));
        }

        [TestMethod]
        public void ExecuteCommand_EventAddedToStore()
        {
            _handler.Execute(_cmd);
            _mockEventStore.Received().AddToEventQueue(Arg.Is<PackageAddedEvent>(e => e.PackageId == _cmd.Package.Id));
 
        }

        private bool IsPackageDataCorrect(Package package, PackageDto packageDto)
        {
            var isCorrect = true;
            if(package.Id != packageDto.Id) { isCorrect = false; }
            else if(package.Name != packageDto.Name) { isCorrect = false; }
            else if(package.Description != packageDto.Description) { isCorrect = false; }
            //else if(!package.LinkMap.Select(linker => linker.Macro.Id)
            //    .SequenceEqual(macroDtoCollection.Select(macro => macro.Id))) { isCorrect = false; }
            return isCorrect;
        }

    }
}
