using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Topic?> GetTopicWithDetailsAsync(int topicId)
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Include(x => x.Teams)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.TopicId == topicId);
        }

        public async Task<Topic?> GetTopicByCodeAsync(string topicCode, int semesterId)
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .FirstOrDefaultAsync(x => x.TopicCode == topicCode
                                       && x.SemesterId == semesterId);
        }

        public async Task<IEnumerable<Topic>> GetTopicsBySemesterAsync(int semesterId)
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Where(x => x.SemesterId == semesterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetActiveTopicsAsync()
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByCategoryAsync(string category)
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Where(x => x.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByInstructorAsync(int instructorId)
        {
            // Get topics that have teams mentored by the instructor
            // or topics that are in cores taught by the instructor
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Include(x => x.Teams)
                .Where(x => x.Teams.Any(t => t.MentorId == instructorId) ||
                           x.Teams.Any(t => t.Core != null && t.Core.InstructorId == instructorId))
                .Distinct()
                .ToListAsync();
        }

        public async Task<Topic?> GetTopicWithTeamsAndCoresAsync(int topicId)
        {
            return await _dbContext.Topics
                .Include(x => x.Semester)
                .Include(x => x.Teams)
                    .ThenInclude(t => t.Core)
                .Include(x => x.Teams)
                    .ThenInclude(t => t.TeamMembers)
                        .ThenInclude(tm => tm.User)
                .Include(x => x.Teams)
                    .ThenInclude(t => t.Leader)
                .Include(x => x.Teams)
                    .ThenInclude(t => t.Mentor)
                .FirstOrDefaultAsync(x => x.TopicId == topicId);
        }
    }
}