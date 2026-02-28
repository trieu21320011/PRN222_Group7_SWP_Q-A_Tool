using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class QuestionFollowerConfiguration : IEntityTypeConfiguration<QuestionFollower>
    {
        public void Configure(EntityTypeBuilder<QuestionFollower> builder)
        {
            builder.HasKey(x => x.QuestionFollowerId);
            builder.Property(x => x.QuestionFollowerId).ValueGeneratedOnAdd();

            builder.Property(x => x.NotifyOnAnswer).HasDefaultValue(true);
            builder.Property(x => x.NotifyOnComment).HasDefaultValue(true);
            builder.Property(x => x.FollowedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Question)
                .WithMany(x => x.QuestionFollowers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.QuestionFollowers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
