using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.QuestionId);
            builder.Property(x => x.QuestionId).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Body).IsRequired();
            builder.Property(x => x.Excerpt).HasMaxLength(1000);
            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.Category).HasMaxLength(100);
            builder.Property(x => x.Difficulty).HasMaxLength(50);
            builder.Property(x => x.ClosedReason).HasMaxLength(500);
            builder.Property(x => x.ViewCount).HasDefaultValue(0);
            builder.Property(x => x.AnswerCount).HasDefaultValue(0);
            builder.Property(x => x.CommentCount).HasDefaultValue(0);
            builder.Property(x => x.IsPinned).HasDefaultValue(false);
            builder.Property(x => x.IsClosed).HasDefaultValue(false);
            builder.Property(x => x.IsPrivate).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.LastActivityAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Author)
                .WithMany(x => x.QuestionAuthors)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ClosedBy)
                .WithMany(x => x.QuestionClosedBies)
                .HasForeignKey(x => x.ClosedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Core)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.CoreId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.AssignedInstructor)
                .WithMany(x => x.QuestionAssignedInstructors)
                .HasForeignKey(x => x.AssignedInstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Topic)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
