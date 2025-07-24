using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zattini.Domain.Entities;

namespace Zattini.Infra.Data.Maps
{
    public class UserAddressMap : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("tb_zattini_user_addresses");

            builder.HasKey(e => e.Id)
                .HasName("pk_zattini_user_addresses");

            builder.Property(e => e.Id)
               .IsRequired()
               .HasColumnType("uuid")
               .HasColumnName("user_addresses_id");

            builder.Property(e => e.Cep)
               .IsRequired()
               .HasColumnName("cep")
               .HasMaxLength(9);

            builder.Property(e => e.TypeAddress)
               .IsRequired()
               .HasColumnName("type_address")
               .HasMaxLength(100);

            builder.Property(e => e.Address)
               .IsRequired()
               .HasColumnName("address")
               .HasMaxLength(200);

            builder.Property(e => e.Number)
               .IsRequired()
               .HasColumnName("number");

            builder.Property(e => e.Complement)
               .IsRequired(false)
               .HasColumnName("complement")
               .HasMaxLength(200);

            builder.Property(e => e.Neighborhood)
               .IsRequired()
               .HasColumnName("neighborhood")
               .HasMaxLength(200);

            builder.Property(e => e.State)
               .IsRequired()
               .HasColumnName("state")
               .HasMaxLength(200);

            builder.Property(e => e.City)
              .IsRequired()
              .HasColumnName("city")
              .HasMaxLength(200);

            builder.Property(e => e.ReferencePoint)
              .IsRequired(false)
              .HasColumnName("reference_point")
              .HasMaxLength(200);

            builder.Property(e => e.UserId)
               .IsRequired()
              .HasColumnName("user_id");

            builder.HasOne(e => e.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_zattini_user_addresses_user");
        }
    }
}
