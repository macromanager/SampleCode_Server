using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using MacroContext.Domain;
using MacroContext.Persistance;
using System.Data;
using MacroContext.ApplicationServices.InternalExceptions;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMacroContextDb _context;
        private IMacroRepository _macros;
        private IPackageRepository _packages;
        private IMacroProfileRepository _macroProfiles;
        private IUserRepository _users;
        private IReferenceProfileRepository _referenceProfiles;


        public IMacroRepository Macros { get { return _macros; } }
        public IPackageRepository Packages { get { return _packages; } }
        public IMacroProfileRepository MacroProfiles { get { return _macroProfiles; } }
        public IUserRepository Users { get { return _users; } }
        public IReferenceProfileRepository ReferenceProfiles { get { return _referenceProfiles; } }


        public UnitOfWork(
            IMacroContextDb context, 
            MacroRepository macroRepo, 
            PackageRepository packagesRepo, 
            MacroProfileRepository macroProfileRepo,
            UserRepository userRepo,
            ReferenceProfileRepository referenceProfileRepo
            )
        {
            macroRepo.SetContext(context);
            packagesRepo.SetContext(context);
            macroProfileRepo.SetContext(context);
            userRepo.SetContext(context);
            referenceProfileRepo.SetContext(context);
           

            _context = context;
            _macros = macroRepo;
            _packages = packagesRepo;
            _macroProfiles = macroProfileRepo;
            _users = userRepo;
            _referenceProfiles = referenceProfileRepo;
        }



        //private void RepositoryFactory(DbContext context, PatientRepository patientRepository, PatientVisitRepository patientVisitRepository, HealthIdRepository healthIdRepository, ContactInfoRepository contactInfoRepository)

        //private void RepositoryFactory(DbContext context)
        //{

        //    _patients = new PatientRepository(context);
        //    _patientVisits = new PatientVisitRepository(context);
        //    _healthcards = new HealthIdRepository(context);
        //    _contactInfo = new ContactInfoRepository(context);
        //}

        public int Complete(bool ignoreConcurrency = false)
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ignoreConcurrency)
                {
                    throw new OptimisticConcurrencyException("Another user has updated that entry", e);
                }
                foreach(var entry in e.Entries)
                {
                    var databaseValues = entry.GetDatabaseValues();
                    entry.OriginalValues.SetValues(databaseValues);
                }
                return this.Complete(true);                
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var entityIds = new List<Guid>();
                var entites = e.Entries;
                foreach (var entry in e.Entries)
                {
                    var entity = entry.Entity as IEntity<Guid>;
                    entityIds.Add(entity.Id);
                }
                throw new EntityAlreadyExistsException(entityIds.ToArray());
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
