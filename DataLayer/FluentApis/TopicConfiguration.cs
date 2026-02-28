using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(x => x.TopicId);
            builder.Property(x => x.TopicId).ValueGeneratedOnAdd();

            builder.Property(x => x.TopicCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TopicName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.Category).HasMaxLength(100);
            builder.Property(x => x.Difficulty).HasMaxLength(50);
            builder.Property(x => x.MaxTeams).HasDefaultValue(0);
            builder.Property(x => x.CurrentTeams).HasDefaultValue(0);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Semester)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
