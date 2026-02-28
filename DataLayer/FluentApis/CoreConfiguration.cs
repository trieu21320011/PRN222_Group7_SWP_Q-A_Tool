using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class CoreConfiguration : IEntityTypeConfiguration<Core>
    {
        public void Configure(EntityTypeBuilder<Core> builder)
        {
            builder.HasKey(x => x.CoreId);
            builder.Property(x => x.CoreId).ValueGeneratedOnAdd();

            builder.Property(x => x.CoreCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CoreName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Schedule).HasMaxLength(500);
            builder.Property(x => x.Room).HasMaxLength(100);
            builder.Property(x => x.MaxTeams).HasDefaultValue(0);
            builder.Property(x => x.CurrentTeams).HasDefaultValue(0);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Instructor)
                .WithMany(x => x.Cores)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Semester)
                .WithMany(x => x.Cores)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
