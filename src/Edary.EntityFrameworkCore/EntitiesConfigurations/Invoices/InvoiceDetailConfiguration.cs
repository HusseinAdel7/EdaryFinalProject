using Edary.Entities.Invoices;
using Edary.Entities.Items;
using Edary.Consts.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Invoices
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetails", schema: "Invoices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitName)
                .IsRequired()
                .HasMaxLength(InvoiceDetailConsts.MaxUnitNameLength);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice).IsRequired();

            builder.Property(x => x.TaxAmount)
                .HasComputedColumnSql("((([Quantity]*[UnitPrice]-[Discount])*[TaxRate])/(100))", stored: true);

            builder.Property(x => x.TotalBeforeTax)
                .HasComputedColumnSql("([Quantity]*[UnitPrice]-[Discount])", stored: true);

            builder.Property(x => x.TotalWithTax)
                .HasComputedColumnSql("(([Quantity]*[UnitPrice]-[Discount])+(([Quantity]*[UnitPrice]-[Discount])*[TaxRate])/(100))", stored: true);

            // InvoiceDetail -> Item
            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

