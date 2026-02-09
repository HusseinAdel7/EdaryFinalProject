using Edary.Entities.SubAccounts;
using Edary.Consts.SubAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.SubAccounts
{
    public class SubAccountConfiguration : IEntityTypeConfiguration<SubAccount>
    {
        public void Configure(EntityTypeBuilder<SubAccount> builder)
        {
            builder.ToTable("SubAccounts", schema: "Accounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountNumber)
                .IsRequired()
                .HasMaxLength(SubAccountConsts.MaxAccountNumberLength);

            builder.HasIndex(x => x.AccountNumber).IsUnique();

            builder.Property(x => x.AccountName)
                .IsRequired()
                .HasMaxLength(SubAccountConsts.MaxAccountNameLength);

            builder.Property(x => x.Title)
                .HasMaxLength(SubAccountConsts.MaxTitleLength);

            builder.Property(x => x.AccountType)
                .HasMaxLength(SubAccountConsts.MaxAccountTypeLength);

            builder.Property(x => x.StandardCreditRate)
                .HasMaxLength(SubAccountConsts.MaxStandardCreditRateLength);

            builder.Property(x => x.AccountCurrency)
                .HasMaxLength(SubAccountConsts.MaxAccountCurrencyLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(SubAccountConsts.MaxNotesLength);

            builder.Property(x => x.AccountNameEn)
                .HasMaxLength(SubAccountConsts.MaxAccountNameEnLength);

            builder.Property(x => x.TitleEn)
                .HasMaxLength(SubAccountConsts.MaxTitleEnLength);

            builder.Property(x => x.AccountTypeEn)
                .HasMaxLength(SubAccountConsts.MaxAccountTypeEnLength);

            builder.Property(x => x.AccountCurrencyEn)
                .HasMaxLength(SubAccountConsts.MaxAccountCurrencyEnLength);

            // SubAccount -> MainAccount
            builder.HasOne(x => x.MainAccount)
                .WithMany()
                .HasForeignKey(x => x.MainAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
