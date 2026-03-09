using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Comment>> GetCommentsByQuestionAsync(int questionId)
        {
            return await _dbContext.Comments
                .Where(c => c.QuestionId == questionId)
                .Include(c => c.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByAnswerAsync(int answerId)
        {
            return await _dbContext.Comments
                .Where(c => c.AnswerId == answerId)
                .Include(c => c.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByAuthorAsync(int authorId)
        {
            return await _dbContext.Comments
                .Where(c => c.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
