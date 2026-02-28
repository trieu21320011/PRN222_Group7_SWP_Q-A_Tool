using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
    {
        public void Configure(EntityTypeBuilder<NotificationRecipient> builder)
        {
            builder.HasKey(x => x.NotificationRecipientId);
            builder.Property(x => x.NotificationRecipientId).ValueGeneratedOnAdd();

            builder.Property(x => x.IsRead).HasDefaultValue(false);
            builder.Property(x => x.IsEmailSent).HasDefaultValue(false);

            builder.HasOne(x => x.Notification)
                .WithMany(x => x.NotificationRecipients)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.NotificationRecipients)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
