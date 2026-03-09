using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class QuestionFollowerRepository : GenericRepository<QuestionFollower>, IQuestionFollowerRepository
    {
        public QuestionFollowerRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<QuestionFollower>> GetFollowersByQuestionAsync(int questionId)
        {
            return await _dbContext.QuestionFollowers
                .Where(f => f.QuestionId == questionId)
                .Include(f => f.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<QuestionFollower>> GetFollowedQuestionsByUserAsync(int userId)
        {
            return await _dbContext.QuestionFollowers
                .Where(f => f.UserId == userId)
                .Include(f => f.Question)
                .ToListAsync();
        }

        public async Task<QuestionFollower?> GetFollowerAsync(int questionId, int userId)
        {
            return await _dbContext.QuestionFollowers
                .FirstOrDefaultAsync(f => f.QuestionId == questionId && f.UserId == userId);
        }

        public async Task<bool> IsFollowingAsync(int questionId, int userId)
        {
            return await _dbContext.QuestionFollowers
                .AnyAsync(f => f.QuestionId == questionId && f.UserId == userId);
        }
    }
}
