using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zattini.Domain.Entities;

namespace Zattini.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_zattini_users");

            builder.HasKey(e => e.Id)
                .HasName("pk_user");

            builder.Property(e => e.Id)
               .IsRequired()
               .HasColumnType("uuid")
               .HasColumnName("user_id");

            builder.Property(e => e.Name)
               .IsRequired()
               .HasColumnName("name")
               .HasMaxLength(100);

            builder.Property(e => e.LastName)
               .IsRequired()
               .HasColumnName("last_name")
               .HasMaxLength(100);

            builder.Property(e => e.Gender)
               .IsRequired()
               .HasColumnName("gender")
               .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(299);

            builder.Property(e => e.BirthDate)
                .IsRequired()
                .HasColumnName("birth_date");

            builder.Property(e => e.Cpf)
                .IsRequired()
                .HasColumnName("cpf")
                .HasMaxLength(14);

            builder.Property(e => e.CellPhone)
                .IsRequired()
                .HasColumnName("cell_phone")
                .HasMaxLength(15);

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasColumnName("password_hash");

            builder.Property(e => e.Salt)
                .IsRequired()
                .HasColumnName("salt");

            builder.Property(e => e.UserImage)
            .IsRequired(false)
            .HasColumnName("user_image");


        }
    }
}
