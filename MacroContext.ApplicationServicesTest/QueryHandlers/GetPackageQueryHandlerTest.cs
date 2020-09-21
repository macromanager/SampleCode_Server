using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Contract.Dto;
using MacroContext.Persistance;
using NSubstitute;
using System.Collections.Generic;
using MacroContext.Domain;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Shared.Utilities;
using MacroContext.Contract.Queries;

namespace ApplicationServices.Test.QueryHandlers
{
    [TestClass]
    public class GetPackageQueryHandlerTest
    {
        private GetPackageQueryHandler _handler;
        private IUnitOfWork _mockUow;
        private IMyMapper _mockMapper;
        private IPackageRepository _mockRepo;
        private GetPackageQuery _query;
        private PackageDto _packageDto;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<IPackageRepository>();
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.Packages.Returns(_mockRepo);
            _packageDto = new PackageDto();

            _mockMapper = Substitute.For<IMyMapper>();
            _mockMapper.Map<Package, PackageDto>(Arg.Any<Package>())
                .Returns(_packageDto);

            _query = new GetPackageQuery();
            _handler = new GetPackageQueryHandler(_mockUow, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_RepoQueriesDataWithCorrectId()
        {
            _handler.Handle(_query);
            _mockRepo.Received().Get(Arg.Is(_query.PackageId));
        }

        [TestMethod]
        public void HandleQuery_PackageIsMappedToDto()
        {
            var data = _handler.Handle(_query);
            Assert.AreSame(data, _packageDto);

        }

    }
}
