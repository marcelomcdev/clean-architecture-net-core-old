using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToroBank.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //public override int SaveChanges()
        //{
        //    AddAuitInfo();
        //    return base.SaveChanges();
        //}

        //public async Task<int> SaveChangesAsync()
        //{
        //    AddAuitInfo();
        //    return await base.SaveChangesAsync();
        //}

        //private void AddAuitInfo()
        //{
        //    var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
        //        }
        //        ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
        //    }
        //}
    }
}
