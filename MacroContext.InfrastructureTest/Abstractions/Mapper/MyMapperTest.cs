using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using MacroContext.Infrastructure.Abstractions.Mapper;
using NSubstitute;
using MacroContext.Domain;
using MacroContext.Contract.Dto;

namespace MacroContext.InfrastructureTest.Abstractions.Mapper
{
    [TestClass]
    public class MyMapperTest
    {
        private MyMapper _myMapper;
        private IMapper _mockMapper;
        private Package _pkg;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMapper = Substitute.For<IMapper>();
            _myMapper = new MyMapper(_mockMapper);
        }
        [TestMethod]
        public void Map_CallsMapper()
        {
            _myMapper.Map<Package, PackageDto>(_pkg);
            _mockMapper.Received().Map<Package,PackageDto>(_pkg);

        }
    }
}
