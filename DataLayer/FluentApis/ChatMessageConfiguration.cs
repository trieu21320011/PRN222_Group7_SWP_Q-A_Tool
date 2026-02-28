using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.ChatMessageId);
            builder.Property(x => x.ChatMessageId).ValueGeneratedOnAdd();

            builder.Property(x => x.MessageText).IsRequired();
            builder.Property(x => x.MessageType).HasMaxLength(50);
            builder.Property(x => x.IsEdited).HasDefaultValue(false);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.ChatRoom)
                .WithMany(x => x.ChatMessages)
                .HasForeignKey(x => x.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.ChatMessages)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReplyToMessage)
                .WithMany(x => x.InverseReplyToMessage)
                .HasForeignKey(x => x.ReplyToMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
