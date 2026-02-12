using Edary.Entities.Invoices;
using Edary.Entities.Suppliers;
using Edary.Entities.Warehouses;
using Edary.Entities.JournalEntries;
using Edary.Consts.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Invoices
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices", schema: "Invoices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(InvoiceConsts.MaxInvoiceNumberLength);

            builder.HasIndex(x => x.InvoiceNumber).IsUnique();

            builder.Property(x => x.InvoiceType)
                .IsRequired()
                .HasMaxLength(InvoiceConsts.MaxInvoiceTypeLength);


            builder.Property(x => x.Currency)
                .HasMaxLength(InvoiceConsts.MaxCurrencyLength);

            builder.Property(x => x.PaymentStatus)
                .HasMaxLength(InvoiceConsts.MaxPaymentStatusLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(InvoiceConsts.MaxNotesLength);

            builder.Property(x => x.NetAmount)
                .HasComputedColumnSql("([TotalAmount]-[Discount])", stored: true);

            builder.Property(x => x.GrandTotal)
                .HasComputedColumnSql("(([TotalAmount]-[Discount])+[TaxAmount])", stored: true);

            // Invoice -> InvoiceDetails
            builder.HasMany(x => x.InvoiceDetails)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice -> Supplier
            builder.HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice -> Warehouse
            builder.HasOne(x => x.Warehouse)
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice -> JournalEntry
            builder.HasOne(x => x.JournalEntry)
                .WithMany()
                .HasForeignKey(x => x.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
