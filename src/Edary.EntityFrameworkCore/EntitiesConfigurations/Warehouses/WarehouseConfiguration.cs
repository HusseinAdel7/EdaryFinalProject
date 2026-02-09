using Edary.Entities.Warehouses;
using Edary.Entities.SubAccounts;
using Edary.Consts.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Warehouses
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses", schema: "Warehouses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WarehouseCode)
                .IsRequired()
                .HasMaxLength(WarehouseConsts.MaxWarehouseCodeLength);

            builder.HasIndex(x => x.WarehouseCode).IsUnique();

            builder.Property(x => x.WarehouseName)
                .IsRequired()
                .HasMaxLength(WarehouseConsts.MaxWarehouseNameLength);

            builder.Property(x => x.Location)
                .HasMaxLength(WarehouseConsts.MaxLocationLength);

            builder.Property(x => x.ManagerName)
                .HasMaxLength(WarehouseConsts.MaxManagerNameLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(WarehouseConsts.MaxNotesLength);

            builder.Property(x => x.IsActive).IsRequired();

            builder.Property(x => x.WarehouseNameEn)
                .HasMaxLength(WarehouseConsts.MaxWarehouseNameEnLength);

            builder.Property(x => x.ManagerNameEn)
                .HasMaxLength(WarehouseConsts.MaxManagerNameEnLength);

        }
    }
}
