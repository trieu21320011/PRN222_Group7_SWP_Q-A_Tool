using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.HasKey(x => x.SemesterId);
            builder.Property(x => x.SemesterId).ValueGeneratedOnAdd();

            builder.Property(x => x.SemesterCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.SemesterName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsCurrent).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
