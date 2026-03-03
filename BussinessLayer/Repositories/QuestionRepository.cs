using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Question?> GetQuestionWithDetailsAsync(int questionId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                    .ThenInclude(c => c.Instructor)
                .Include(x => x.Topic)
                .Include(x => x.AssignedInstructor)
                .Include(x => x.ClosedBy)
                .Include(x => x.Answers)
                    .ThenInclude(a => a.Author)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(x => x.QuestionId == questionId);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByAuthorAsync(int authorId)
        {
            return await _dbContext.Questions
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                .Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTeamAsync(int teamId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                    .ThenInclude(a => a.Author)
                .Include(x => x.AssignedInstructor)
                .Where(x => x.TeamId == teamId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTopicAsync(int topicId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Answers)
                .Where(x => x.TopicId == topicId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByCoreAsync(int coreId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                    .ThenInclude(a => a.Author)
                .Include(x => x.AssignedInstructor)
                .Where(x => x.CoreId == coreId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByCategoryAsync(string category)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Where(x => x.Category == category)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetPinnedQuestionsAsync()
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Where(x => x.IsPinned == true)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetRecentQuestionsAsync(int count)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetOpenQuestionsAsync()
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                .Where(x => x.IsClosed != true && x.Status == "Open")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTeamInCoreAsync(int teamId, int coreId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                    .ThenInclude(a => a.Author)
                .Include(x => x.AssignedInstructor)
                .Where(x => x.TeamId == teamId && x.CoreId == coreId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsAssignedToInstructorAsync(int instructorId)
        {
            return await _dbContext.Questions
                .Include(x => x.Author)
                .Include(x => x.Team)
                .Include(x => x.Core)
                .Include(x => x.Topic)
                .Include(x => x.Answers)
                .Where(x => x.AssignedInstructorId == instructorId || 
                           (x.Core != null && x.Core.InstructorId == instructorId))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task MarkAsResolvedAsync(int questionId, int closedById)
        {
            var question = await _dbContext.Questions.FindAsync(questionId);
            if (question != null)
            {
                question.IsClosed = true;
                question.Status = "Resolved";
                question.ClosedById = closedById;
                question.ClosedAt = DateTime.Now;
                question.UpdatedAt = DateTime.Now;
                _dbContext.Questions.Update(question);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
