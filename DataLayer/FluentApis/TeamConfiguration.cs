using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.TeamId);
            builder.Property(x => x.TeamId).ValueGeneratedOnAdd();

            builder.Property(x => x.TeamName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TeamCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.ProjectName).HasMaxLength(500);
            builder.Property(x => x.Semester).HasMaxLength(50);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Leader)
                .WithMany(x => x.TeamLeaders)
                .HasForeignKey(x => x.LeaderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Mentor)
                .WithMany(x => x.TeamMentors)
                .HasForeignKey(x => x.MentorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Core)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.CoreId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Topic)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.SemesterNavigation)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
