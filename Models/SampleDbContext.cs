using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Navigation;
using SampleCode.Other;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Models
{
    public class SampleDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }              
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<SuburbModel> Suburbs { get; set; }
        public DbSet<StreetTypeModel> StreetTypes { get; set; }
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<RouteAddressModel> RouteAddresses { get; set; }
            
        public SampleDbContext()
        {
            Debugger.Launch();            
            //Database.EnsureCreated();
        }

        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            Debug.WriteLine("-- OnConfiguring --");            
            optionsBuilder.UseSqlite("Data Source=database70.db");
            optionsBuilder.EnableSensitiveDataLogging(true);                       
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine("-- OnModelCreating --");
            modelBuilder.SetupContext(this.Database.IsSqlite()); // Identify if we are using a SQLite database
            modelBuilder.Entity<UserModel>().HasIndex(x => x.Username).IsUnique();            
            

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {                
                //https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }                
            }
        }
    }

    public static class DbSetExtensions
    {
        public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            Debug.WriteLine("Adding db extension");
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }
    }

    public static class ContextSetup
    {
        public static void SetupContext(this ModelBuilder modelBuilder, bool isSQLite)
        {
            Debug.WriteLine("In SetupContext");
            // Model building ...

            // Handle datetimes in SQLite src: https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
            if (isSQLite) // We found this in Context.cs
            {
                Debug.WriteLine("is using sqlite");
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter()); // The converter!
                    }
                }
            }
        }
    }
}