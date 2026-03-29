using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Team?> GetTeamWithDetailsAsync(int teamId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                    .ThenInclude(c => c.Instructor)
                .Include(x => x.Topic)
                .Include(x => x.SemesterNavigation)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                .FirstOrDefaultAsync(x => x.TeamId == teamId);
        }

        public async Task<Team?> GetTeamByCodeAsync(string teamCode)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .FirstOrDefaultAsync(x => x.TeamCode == teamCode);
        }

        public async Task<IEnumerable<Team>> GetTeamsByLeaderAsync(int leaderId)
        {
            return await _dbContext.Teams
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.SemesterNavigation)
                .Where(x => x.LeaderId == leaderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsByMentorAsync(int mentorId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.SemesterNavigation)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                .Where(x => x.MentorId == mentorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetActiveTeamsAsync()
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsBySemesterAsync(int semesterId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Where(x => x.SemesterId == semesterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsByCoreAsync(int coreId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Topic)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                .Where(x => x.CoreId == coreId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsByTopicAsync(int topicId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                .Where(x => x.TopicId == topicId)
                .ToListAsync();
        }

        public async Task<Team?> GetTeamWithMembersAsync(int teamId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                        .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(x => x.TeamId == teamId);
        }

        public async Task<Team?> GetTeamWithQuestionsAsync(int teamId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Questions)
                    .ThenInclude(q => q.Author)
                .Include(x => x.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(x => x.TeamId == teamId);
        }

        public async Task<Team?> GetTeamProgressAsync(int teamId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.TeamMembers)
                    .ThenInclude(tm => tm.User)
                .Include(x => x.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(x => x.TeamId == teamId);
        }

        public async Task<IEnumerable<Team>> GetTeamsByUserAsync(int userId)
        {
            return await _dbContext.Teams
                .Include(x => x.Leader)
                .Include(x => x.Mentor)
                .Include(x => x.Core)
                    .ThenInclude(c => c.Semester)
                .Include(x => x.Topic)
                .Include(x => x.SemesterNavigation)
                .Where(x => x.TeamMembers.Any(tm => tm.UserId == userId) || x.LeaderId == userId)
                .ToListAsync();
        }

        public async Task<bool> AddMemberToTeamAsync(int teamId, int userId, string role)
        {
            // Check if user is already a member of this team
            var existingMember = await _dbContext.TeamMembers
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);

            if (existingMember != null)
            {
                return false; // User is already a member
            }

            var teamMember = new TeamMember
            {
                TeamId = teamId,
                UserId = userId,
                Role = role,
                JoinedAt = DateTime.UtcNow
            };

            await _dbContext.TeamMembers.AddAsync(teamMember);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
