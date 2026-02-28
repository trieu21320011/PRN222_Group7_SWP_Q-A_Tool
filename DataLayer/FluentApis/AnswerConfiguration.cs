using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.AnswerId);
            builder.Property(x => x.AnswerId).ValueGeneratedOnAdd();

            builder.Property(x => x.Body).IsRequired();
            builder.Property(x => x.IsAccepted).HasDefaultValue(false);
            builder.Property(x => x.IsMentorAnswer).HasDefaultValue(false);
            builder.Property(x => x.CommentCount).HasDefaultValue(0);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
