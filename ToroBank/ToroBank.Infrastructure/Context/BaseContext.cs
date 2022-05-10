using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToroBank.Core.Entities;
using ToroBank.Infrastructure.EntityFramework.Mapping;

namespace ToroBank.Infrastructure.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AssetMap());
            modelBuilder.ApplyConfiguration(new UserAssetMap());
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<UserAsset> UserAsset { get; set; }

    }
}
