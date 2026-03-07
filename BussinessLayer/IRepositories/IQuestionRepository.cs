using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<IEnumerable<Question>> GetAllWithDetailsAsync();
        Task<Question?> GetQuestionWithDetailsAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsByAuthorAsync(int authorId);
        Task<IEnumerable<Question>> GetQuestionsByTeamAsync(int teamId);
        Task<IEnumerable<Question>> GetQuestionsByTopicAsync(int topicId);
        Task<IEnumerable<Question>> GetQuestionsByCoreAsync(int coreId);
        Task<IEnumerable<Question>> GetQuestionsByCategoryAsync(string category);
        Task<IEnumerable<Question>> GetPinnedQuestionsAsync();
        Task<IEnumerable<Question>> GetRecentQuestionsAsync(int count);
        Task<IEnumerable<Question>> GetOpenQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsByTeamInCoreAsync(int teamId, int coreId);
        Task<IEnumerable<Question>> GetQuestionsAssignedToInstructorAsync(int instructorId);
        Task MarkAsResolvedAsync(int questionId, int closedById);
    }
}
