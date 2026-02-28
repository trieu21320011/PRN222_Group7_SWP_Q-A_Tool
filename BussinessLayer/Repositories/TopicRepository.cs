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
    }
}