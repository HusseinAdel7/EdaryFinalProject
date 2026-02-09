using Edary.Entities.Items;
using Edary.Consts.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Items
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", schema: "Items");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ItemCode)
                .IsRequired()
                .HasMaxLength(ItemConsts.MaxItemCodeLength);

            builder.HasIndex(x => x.ItemCode).IsUnique();

            builder.Property(x => x.ItemName)
                .IsRequired()
                .HasMaxLength(ItemConsts.MaxItemNameLength);

            builder.Property(x => x.ItemType)
                .HasMaxLength(ItemConsts.MaxItemTypeLength);

            builder.Property(x => x.GroupName)
                .HasMaxLength(ItemConsts.MaxGroupNameLength);

            builder.Property(x => x.Barcode)
                .HasMaxLength(ItemConsts.MaxBarcodeLength);

            builder.Property(x => x.OpeningPrice).IsRequired();

            builder.Property(x => x.UnitOfMeasure)
                .HasMaxLength(ItemConsts.MaxUnitOfMeasureLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(ItemConsts.MaxNotesLength);

            builder.Property(x => x.ItemNameEn)
                .HasMaxLength(ItemConsts.MaxItemNameEnLength);

            builder.Property(x => x.ItemTypeEn)
                .HasMaxLength(ItemConsts.MaxItemTypeEnLength);

            builder.Property(x => x.GroupNameEn)
                .HasMaxLength(ItemConsts.MaxGroupNameEnLength);

            builder.Property(x => x.UnitOfMeasureEn)
                .HasMaxLength(ItemConsts.MaxUnitOfMeasureEnLength);

            // Item -> ItemPrices
            builder.HasMany(x => x.ItemPrices)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

