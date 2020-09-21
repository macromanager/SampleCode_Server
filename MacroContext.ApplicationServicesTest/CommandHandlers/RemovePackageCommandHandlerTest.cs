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
    public class RemovePackageCommandHandlerTest
    {
        private RemovePackageCommandHandler _handler;
        private RemovePackageCommand _cmd;
        private PackageDto _packageDto;
        private IUnitOfWork _mockUow;
        private IPackageRepository _mockRepo;
        private IEventStore _mockEventStore;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IPackageRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.Packages.Returns(_mockRepo);
            _mockEventStore = Substitute.For<IEventStore>();
            _handler = new RemovePackageCommandHandler(_mockUow, _mockEventStore);
            _packageDto = new PackageDto()
            {
                Id = Guid.NewGuid(),
                Name = "myName",
                Description = "myDescription",

            };



            _cmd = new RemovePackageCommand(_packageDto);
        }

        [TestMethod]
        public void ExecuteCommand_WithValidInputs()
        {
            _handler.Execute(_cmd);
            _mockRepo.Received().Remove(Arg.Is<Package>(
                pkg =>
                IsPackageDataCorrect(pkg, _cmd.Package)
                ));
        }

        [TestMethod]
        public void ExecuteCommand_PackageRemovedEventsIdStored()
        {
            _handler.Execute(_cmd);
            _mockEventStore.Received().AddToEventQueue(Arg.Is<PackageRemovedEvent>(p => p.PackageId == _cmd.Package.Id));
        }

        private bool IsPackageDataCorrect(Package package, PackageDto packageDto)
        {
            var isCorrect = true;
            if(package.Id != packageDto.Id) { isCorrect = false; }
            if(package.RowVersion != packageDto.RowVersion) { isCorrect = false; }
            return isCorrect;
        }

    }
}
