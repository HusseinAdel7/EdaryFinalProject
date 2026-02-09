using Edary.Entities.Suppliers;
using Edary.Entities.SubAccounts;
using Edary.Consts.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.Suppliers
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers", schema: "Suppliers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SupplierCode)
                .IsRequired()
                .HasMaxLength(SupplierConsts.MaxSupplierCodeLength);

            builder.HasIndex(x => x.SupplierCode).IsUnique();

            builder.Property(x => x.SupplierName)
                .IsRequired()
                .HasMaxLength(SupplierConsts.MaxSupplierNameLength);

            builder.Property(x => x.Phone)
                .HasMaxLength(SupplierConsts.MaxPhoneLength);

            builder.Property(x => x.Email)
                .HasMaxLength(SupplierConsts.MaxEmailLength);

            builder.Property(x => x.Address)
                .HasMaxLength(SupplierConsts.MaxAddressLength);

            builder.Property(x => x.TaxNumber)
                .HasMaxLength(SupplierConsts.MaxTaxNumberLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(SupplierConsts.MaxNotesLength);

            builder.Property(x => x.SupplierNameEn)
                .HasMaxLength(SupplierConsts.MaxSupplierNameEnLength);

            // Supplier -> SubAccount
            builder.HasOne(x => x.SubAccount)
                .WithMany()
                .HasForeignKey(x => x.SubAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
