using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).ValueGeneratedOnAdd();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DisplayName).HasMaxLength(200);
            builder.Property(x => x.AvatarUrl).HasMaxLength(1000);
            builder.Property(x => x.Bio).HasMaxLength(2000);
            builder.Property(x => x.StudentId).HasMaxLength(50);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsEmailVerified).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.StudentId).IsUnique();

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
