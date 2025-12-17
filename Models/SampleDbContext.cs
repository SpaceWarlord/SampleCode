using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.Navigation;
using Models.Other;
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
            modelBuilder.Entity<UserModel>().HasIndex(x => x.Username).IsUnique();                                   
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
}