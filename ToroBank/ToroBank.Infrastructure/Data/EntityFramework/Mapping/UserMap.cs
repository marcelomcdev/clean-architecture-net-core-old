using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToroBank.Core.Entities;

namespace ToroBank.Infrastructure.EntityFramework.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.AccountNumber).IsRequired();
            builder.Property(p => p.CPF).IsRequired();
            builder.Property(p => p.Balance).IsRequired();
        }
    }
}
