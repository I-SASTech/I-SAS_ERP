using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class WareHouseTransferConfiguration : IEntityTypeConfiguration<WareHouseTransfer>
  {
      public void Configure(EntityTypeBuilder<WareHouseTransfer> builder)
      {
          builder.HasOne(s => s.Warehouse)
              .WithMany(ad => ad.WareHouseTransfers)
              .HasForeignKey(ad => ad.FromWareHouseId);


          builder.HasOne(s => s.Warehouse)
              .WithMany(ad => ad.WareHouseTransfers)
              .HasForeignKey(ad => ad.ToWareHouseId);


        }
  }
}
