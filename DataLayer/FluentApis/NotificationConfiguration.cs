using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.NotificationId).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.NotificationType).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ReferenceType).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
