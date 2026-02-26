using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByQuestionAsync(int questionId);
        Task<IEnumerable<Comment>> GetCommentsByAnswerAsync(int answerId);
        Task<IEnumerable<Comment>> GetCommentsByAuthorAsync(int authorId);
    }
}
