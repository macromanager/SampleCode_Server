using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Infrastructure.Abstractions.Orm;
using MacroContext.Persistance;
using NSubstitute;

namespace MacroContext.InfrastructureTest.Abstractions.Orm
{
    [TestClass]
    public class UnitOfWorkTest
    {

        private UnitOfWork _uow;
        private PackageRepository _mockPackageRepo;
        private MacroRepository _mockMacroRepo;
        private MacroProfileRepository _mockProfileRepo;
        private UserRepository _mockUserRepo;
        private ReferenceProfileRepository _mockReferenceProfileRepo;
        private IMacroContextDb _mockContext;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockPackageRepo = Substitute.For<PackageRepository>();
            _mockMacroRepo = Substitute.For<MacroRepository>();
            _mockProfileRepo = Substitute.For<MacroProfileRepository>();
            _mockUserRepo = Substitute.For<UserRepository>();
            _mockReferenceProfileRepo = Substitute.For<ReferenceProfileRepository>();


            _mockContext = Substitute.For<IMacroContextDb>();
            _uow = new UnitOfWork(_mockContext, _mockMacroRepo, _mockPackageRepo, _mockProfileRepo, _mockUserRepo, _mockReferenceProfileRepo);

        }




        [TestMethod]
        public void CommitTransasction_TransactionSaved()
        {
            _uow.Complete();
            _mockContext.Received().SaveChanges();

        }

        [TestMethod]
        public void Dispose_ContextDisposed()
        {
            _uow.Dispose();
            _mockContext.Received().Dispose();

        }
    }
}
