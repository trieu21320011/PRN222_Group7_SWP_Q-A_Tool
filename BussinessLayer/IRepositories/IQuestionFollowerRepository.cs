using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface IQuestionFollowerRepository : IGenericRepository<QuestionFollower>
    {
        Task<IEnumerable<QuestionFollower>> GetFollowersByQuestionAsync(int questionId);
        Task<IEnumerable<QuestionFollower>> GetFollowedQuestionsByUserAsync(int userId);
        Task<QuestionFollower?> GetFollowerAsync(int questionId, int userId);
        Task<bool> IsFollowingAsync(int questionId, int userId);
    }
}
