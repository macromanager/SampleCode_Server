using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Domain;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using NSubstitute;
using MacroContext.Infrastructure.Abstractions.Orm;
using System.Configuration;

namespace MacroContext.InfrastructureTest.Abstractions.Orm
{
    [TestClass]
    public class RepositoryTest // tested with inherited type (PackageRepository)
    {
        private PackageRepository _repo;
        private IMacroContextDb _mockContext;
        private DbSet<Package> _mockDbSet;
        //private DbEntityEntry<Package> _mockDbEntityEntry;
        private Package _package;
        private IEnumerable<Package> _packageCollection;



        [TestInitialize]
        public void TestInitialize()
        {
            _package = new Package(Guid.NewGuid());
            _packageCollection = new List<Package>() { new Package(Guid.NewGuid()) };

            _mockContext = Substitute.For<IMacroContextDb>();
            _mockDbSet = MockDbSet.GetFakeDbSet(_packageCollection.AsQueryable());
            //_mockDbEntityEntry = Substitute.For<DbEntityEntry<Package>>();
            _mockContext.Set<Package>().Returns(_mockDbSet);
           // _mockContext.Entry<Package>(Arg.Any<Package>()).Returns(_mockDbEntityEntry);
            //_repo = new PackageRepository(_mockContext);
            _repo = new PackageRepository();
            _repo.SetContext(_mockContext);


        }




        [TestMethod]
        public void Add_WithValidInputs()
        {
            _repo.Add(_package);
            _mockDbSet.Received().Add(_package);

        }

        [TestMethod]
        public void AddRange_WithValidInputs()
        {
            _repo.AddRange(_packageCollection);
            _mockDbSet.Received().AddRange(_packageCollection);
        }




        [TestMethod]
        public void Update_WithValidInputs()
        {
            _repo.Update(_package);
            _mockContext.Received().SetEntityState(_package, EntityState.Modified);
        }

        [TestMethod]
        public void Remove_WithValidInputs()
        {
            _repo.Remove(_package);
            _mockContext.Received().SetEntityState(_package, EntityState.Deleted);
        }

        [TestMethod]
        public void RemoveRange_WithValidInputs()
        {
            _repo.RemoveRange(_packageCollection);
            foreach(var pkg in _packageCollection)
            {
                _mockContext.Received().SetEntityState(pkg, EntityState.Deleted);
            }


        }

        [TestMethod]
        public void Find_WithValidInputs()
        {
            Expression<Func<Package, bool>> predicate = account => account.Id == Guid.NewGuid();
            _repo.FindOne(predicate);
            _mockDbSet.Received().Where(predicate);
        }

        [TestMethod]
        public void Get_WithValidInputs()
        {
            _repo.Get(_package.Id);
            _mockDbSet.Received().Find(_package.Id);
        }

        public void GetAll_WithValidInputs()
        {
            _repo.GetAll(new Shared.ValueObjects.PagingInformation(1,1));
            _mockDbSet.Received().ToList();
        }


        [TestMethod]
        public void Attach_WithValidInputs()
        {
            _repo.Attach(_package);
            _mockDbSet.Received().Attach(_package);
        }

        [TestMethod]
        public void Detach_WithValidInputs()
        {
            _repo.Detach(_package);
            _mockContext.Received().SetEntityState(_package, EntityState.Detached);

        }



    }
}
