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
        Task<Question?> GetQuestionWithDetailsAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsByAuthorAsync(int authorId);
        Task<IEnumerable<Question>> GetQuestionsByTeamAsync(int teamId);
        Task<IEnumerable<Question>> GetQuestionsByTagAsync(int tagId);
        Task<IEnumerable<Question>> GetPinnedQuestionsAsync();
        Task<IEnumerable<Question>> GetRecentQuestionsAsync(int count);
    }
}
