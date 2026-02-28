using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class SystemInstructorConfiguration : IEntityTypeConfiguration<SystemInstructor>
    {
        public void Configure(EntityTypeBuilder<SystemInstructor> builder)
        {
            builder.HasKey(x => x.SystemInstructorId);
            builder.Property(x => x.SystemInstructorId).ValueGeneratedOnAdd();

            builder.Property(x => x.IsHead).HasDefaultValue(false);
            builder.Property(x => x.CanManageTopics).HasDefaultValue(false);
            builder.Property(x => x.CanManageCores).HasDefaultValue(false);
            builder.Property(x => x.CanManageInstructors).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.User)
                .WithMany(x => x.SystemInstructors)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Semester)
                .WithMany(x => x.SystemInstructors)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
