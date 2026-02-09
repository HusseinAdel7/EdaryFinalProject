using Edary.Entities.Items;
using Edary.Consts.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Items
{
    public class ItemPriceConfiguration : IEntityTypeConfiguration<ItemPrice>
    {
        public void Configure(EntityTypeBuilder<ItemPrice> builder)
        {
            builder.ToTable("ItemPrices", schema: "Items");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitName)
                .IsRequired()
                .HasMaxLength(ItemPriceConsts.MaxUnitNameLength);

            builder.Property(x => x.Currency)
                .HasMaxLength(ItemPriceConsts.MaxCurrencyLength);

            builder.Property(x => x.UnitNameEn)
                .HasMaxLength(ItemPriceConsts.MaxUnitNameEnLength);

            builder.Property(x => x.CurrencyEn)
                .HasMaxLength(ItemPriceConsts.MaxCurrencyEnLength);
        }
    }
}

