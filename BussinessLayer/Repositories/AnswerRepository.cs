using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Answer?> GetAnswerWithDetailsAsync(int answerId)
        {
            return await _dbContext.Answers
                .Include(x => x.Author)
                    .ThenInclude(a => a.Role)
                .Include(x => x.Question)
                    .ThenInclude(q => q.Team)
                .Include(x => x.Question)
                    .ThenInclude(q => q.Core)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(x => x.AnswerId == answerId);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId)
        {
            return await _dbContext.Answers
                .Include(x => x.Author)
                    .ThenInclude(a => a.Role)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Author)
                .Where(x => x.QuestionId == questionId)
                .OrderByDescending(x => x.IsAccepted)
                .ThenByDescending(x => x.IsMentorAnswer)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetAnswersByAuthorAsync(int authorId)
        {
            return await _dbContext.Answers
                .Include(x => x.Question)
                    .ThenInclude(q => q.Team)
                .Include(x => x.Question)
                    .ThenInclude(q => q.Core)
                .Include(x => x.Comments)
                .Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Answer?> GetAcceptedAnswerByQuestionAsync(int questionId)
        {
            return await _dbContext.Answers
                .Include(x => x.Author)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.QuestionId == questionId && x.IsAccepted == true);
        }
    }
}
