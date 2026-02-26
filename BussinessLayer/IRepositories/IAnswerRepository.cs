using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
        Task<Answer?> GetAnswerWithDetailsAsync(int answerId);
        Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId);
        Task<IEnumerable<Answer>> GetAnswersByAuthorAsync(int authorId);
        Task<Answer?> GetAcceptedAnswerByQuestionAsync(int questionId);
    }
}
