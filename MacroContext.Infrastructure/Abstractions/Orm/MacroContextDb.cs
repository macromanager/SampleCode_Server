using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MacroContext.Domain;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    [DbConfigurationType(typeof(DbConfig))]
    public class MacroContextDb:DbContext, IMacroContextDb
    {

        static MacroContextDb()
        {
            
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
            {
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
            }
        }

        public void SetModifiedProperty<TEntity, TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TProperty>> property, bool isModified) where TEntity : class, IEntity<Guid>
        {
            this.Entry(entity).Property(property).IsModified = isModified;
        } 

        public MacroContextDb():base(AppSettings.DB_CONNECTION_STRING)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MacroContextDb, Migrations.Configuration>());
            // Database.SetInitializer<MacroContextDb>(null);
            // IDatabaseInitializer<MacroContextDb> dbInitializer = new 
            // Database.SetInitializer<MacroContextDb>(new CreateDatabaseIfNotExists<MacroContextDb>());
            // Database.SetInitializer<MacroContextDb>(new DropCreateDatabaseIfModelChanges<MacroContextDb>());
            // Database.SetInitializer<MacroContextDb>(new DropCreateDatabaseAlways<MacroContextDb>());
        }

        //public MacroContextDb() // for testing
        //{
        //    //Database.SetInitializer<MacroContextDb>(new DropCreateDatabaseAlways<MacroContextDb>());
        //}


        public virtual DbSet<Macro> Macros { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<MacroProfile> MacroProfiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ReferenceProfile> ReferenceProfiles { get; set; }


        public void SetEntityState(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }

        public override int SaveChanges()
        {
            CustomOptimisitcConcurrency(this);
            return base.SaveChanges();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.SetTableNames(modelBuilder);
            this.SetDatabaseGeneratedIdOptions(modelBuilder);
            this.SetRowVersioning(modelBuilder);
            this.SetCustomEntityRelations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CustomOptimisitcConcurrency(DbContext dbContext) // this is to support all providers not just sqlserver
        {
            foreach (var dbEntityEntry in dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                IEntity<Guid> entity = dbEntityEntry.Entity as IEntity<Guid>;
                if (entity != null)
                {

                    if (dbEntityEntry.State == EntityState.Added)
                    {
                        var rowversion = dbEntityEntry.Property("RowVersion");
                        rowversion.CurrentValue = BitConverter.GetBytes((Int64)1);

                        //var rowversion = entity.RowVersion;

                    }
                    else if (dbEntityEntry.State == EntityState.Modified)
                    {
                        var valueBefore = new byte[8];
                        System.Array.Copy(dbEntityEntry.OriginalValues.GetValue<byte[]>("RowVersion"), valueBefore, 8);

                        var value = BitConverter.ToInt64(entity.RowVersion, 0);
                        if (value == Int64.MaxValue)
                            value = 1;
                        else value++;

                        var rowversion = dbEntityEntry.Property("RowVersion");
                        rowversion.CurrentValue = BitConverter.GetBytes((Int64)value);
                        rowversion.OriginalValue = valueBefore;//This is the magic line!!

                    }

                }
            }
        }



        private void SetCustomEntityRelations(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<MacroProfile>()
                .HasRequired(profile => profile.Macro).WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ReferenceProfile>()
                .Property(prof => prof.ReferenceId);

            modelBuilder.ComplexType<ReferenceVersion>();

            modelBuilder.Entity<Package>()
                .HasMany(pkg => pkg.MacroProfiles)
                .WithRequired().HasForeignKey(profile => profile.PackageId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Package>()
                .HasMany(p => p.ReferenceProfiles)
                .WithRequired().HasForeignKey(refProfile => refProfile.PackageId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Package>()
                .Property(p => p.UserId).IsRequired();

          
            //modelBuilder.Entity<Package>(b => {
            //    b.HasKey(c => c.Id);
            //    b.Property(c => c.ParentId).IsRequired();


            //.HasOne<Parent>()    // <---
            //    .WithMany()       // <---
            //    .HasForeignKey(c => c.ParentId);

        }


        private void SetTableNames(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Macro>().ToTable("Macros");
            modelBuilder.Entity<Package>().ToTable("Packages");
            modelBuilder.Entity<MacroProfile>().ToTable("MacroProfiles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<ReferenceProfile>().ToTable("ReferenceProfiles");

        }

        private void SetRowVersioning(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Macro>().Property(s => s.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Package>().Property(s => s.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MacroProfile>().Property(s => s.RowVersion).IsRowVersion();
            //modelBuilder.Entity<User>().Property(s => s.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ReferenceProfile>().Property(s => s.RowVersion).IsRowVersion();

            modelBuilder.Entity<Macro>().Property(s => s.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<Package>().Property(s => s.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<MacroProfile>().Property(s => s.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<User>().Property(s => s.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<ReferenceProfile>().Property(s => s.RowVersion).IsConcurrencyToken();
        }

        private void SetDatabaseGeneratedIdOptions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Macro>()
                .Property(macro => macro.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<Package>()
                .Property(package => package.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<MacroProfile>()
                .Property(profile => profile.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<User>()
               .Property(user => user.Id)
               .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<ReferenceProfile>()
            .Property(refProf => refProf.Id)
            .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

        }

    }
}
