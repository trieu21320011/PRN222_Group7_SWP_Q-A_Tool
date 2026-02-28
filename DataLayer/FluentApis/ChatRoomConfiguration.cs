using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(x => x.ChatRoomId);
            builder.Property(x => x.ChatRoomId).ValueGeneratedOnAdd();

            builder.Property(x => x.RoomName).HasMaxLength(200);
            builder.Property(x => x.RoomType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.ChatRooms)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.ChatRooms)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
