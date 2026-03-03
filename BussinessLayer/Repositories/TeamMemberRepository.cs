using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<TeamMember?> GetTeamMemberWithDetailsAsync(int teamMemberId)
        {
            return await _dbContext.TeamMembers
                .Include(x => x.Team)
                    .ThenInclude(t => t.Core)
                .Include(x => x.Team)
                    .ThenInclude(t => t.Topic)
                .Include(x => x.User)
                    .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(x => x.TeamMemberId == teamMemberId);
        }

        public async Task<IEnumerable<TeamMember>> GetMembersByTeamAsync(int teamId)
        {
            return await _dbContext.TeamMembers
                .Include(x => x.User)
                    .ThenInclude(u => u.Role)
                .Include(x => x.Team)
                .Where(x => x.TeamId == teamId)
                .OrderBy(x => x.Role)
                .ThenBy(x => x.JoinedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeamMember>> GetTeamsByUserAsync(int userId)
        {
            return await _dbContext.TeamMembers
                .Include(x => x.Team)
                    .ThenInclude(t => t.Core)
                .Include(x => x.Team)
                    .ThenInclude(t => t.Topic)
                .Include(x => x.Team)
                    .ThenInclude(t => t.Leader)
                .Include(x => x.Team)
                    .ThenInclude(t => t.Mentor)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<TeamMember?> GetMembershipAsync(int teamId, int userId)
        {
            return await _dbContext.TeamMembers
                .Include(x => x.Team)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TeamId == teamId && x.UserId == userId);
        }
    }
}
