using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToroBank.Core.Entities;

namespace ToroBank.Infrastructure.EntityFramework.Mapping
{
    public class UserAssetMap : IEntityTypeConfiguration<UserAsset>
    {
        public void Configure(EntityTypeBuilder<UserAsset> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.AssetId).IsRequired();
        }
    }

}
