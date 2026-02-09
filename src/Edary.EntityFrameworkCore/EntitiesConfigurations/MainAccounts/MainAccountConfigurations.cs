using Edary.Entities.MainAccounts;
using Edary.Consts.MainAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Edary.EntityFrameworkCore.EntitiesConfigurations.MainAccounts
{
    public class MainAccountConfigurations : IEntityTypeConfiguration<MainAccount>
    {
        public void Configure(EntityTypeBuilder<MainAccount> builder)
        {
            builder.ToTable("MainAccounts", schema: "Accounts");

            builder.ConfigureByConvention();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountNumber)
                .IsRequired()
                .HasMaxLength(MainAccountConsts.MaxAccountNumberLength);

            builder.Property(x => x.AccountName)
                .IsRequired()
                .HasMaxLength(MainAccountConsts.MaxAccountNameLength);

            builder.Property(x => x.AccountNameEn)
                .HasMaxLength(MainAccountConsts.MaxAccountNameEnLength);

            builder.Property(x => x.Title)
                .HasMaxLength(MainAccountConsts.MaxTitleLength);

            builder.Property(x => x.TitleEn)
                .HasMaxLength(MainAccountConsts.MaxTitleEnLength);

            builder.Property(x => x.TransferredTo)
                .IsRequired()
                .HasMaxLength(MainAccountConsts.MaxTransferredToLength);

            builder.Property(x => x.TransferredToEn)
                .HasMaxLength(MainAccountConsts.MaxTransferredToEnLength);

            builder.Property(x => x.Notes)
                .HasMaxLength(MainAccountConsts.MaxNotesLength);


            builder.HasIndex(x => x.AccountNumber);
            builder.HasIndex(x => x.TenantId);

          
        }
    }
}
