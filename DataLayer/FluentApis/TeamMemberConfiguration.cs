using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.FluentApis
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(x => x.TeamMemberId);
            builder.Property(x => x.TeamMemberId).ValueGeneratedOnAdd();

            builder.Property(x => x.Role).HasMaxLength(100);
            builder.Property(x => x.JoinedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Team)
                .WithMany(x => x.TeamMembers)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.TeamMembers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
