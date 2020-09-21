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
using MacroContext.Shared.ValueObjects;

namespace ApplicationServices.Test.QueryHandlers
{
    [TestClass]
    public class GetAllPackagesQueryHandlerTest
    {
        private GetAllPackagesQueryHandler _handler;
        private IUnitOfWork _mockUow;
        private IMyMapper _mockMapper;
        private IPackageRepository _mockRepo;
        private GetAllPackagesQuery _query;
        private PackageDto[] _packageDtos;
        private Package[] _packages = new Package[0];
        private PagedResult<Package> _repoResult;
        private PagedResult<PackageDto> _queryResult;

        [TestInitialize]
        public void TestInitialize()
        {
            _repoResult = new PagedResult<Package>(new PagingInformation(1, 1), 1, 1, _packages);
            _mockRepo = Substitute.For<IPackageRepository>();
            _mockRepo.GetAll(Arg.Any<PagingInformation>()).Returns(_repoResult);
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.Packages.Returns(_mockRepo);
            _packageDtos = new PackageDto[0];

            _mockMapper = Substitute.For<IMyMapper>();
            _mockMapper.Map<Package[], PackageDto[]>(Arg.Any<Package[]>())
                .Returns(_packageDtos);

            _query = new GetAllPackagesQuery(new PagingInformation(1,1));
            _queryResult = new PagedResult<PackageDto>(new PagingInformation(1, 1), 1, 1, _packageDtos);
            _handler = new GetAllPackagesQueryHandler(_mockUow, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_RepoPerformsQuery()
        {

            _handler.Handle(_query);
            _mockRepo.Received().GetAll(NSubstitute.Arg.Any<PagingInformation>());
        }

        [TestMethod]
        public void HandleQuery_PackagesAreMappedToDtos()
        {
            var data = _handler.Handle(_query);
            Assert.AreSame(data.Result, _queryResult.Result);
        }

    }
}
