using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ITopicRepository : IGenericRepository<Topic>
    {
        Task<Topic?> GetTopicWithDetailsAsync(int topicId);
        Task<Topic?> GetTopicByCodeAsync(string topicCode, int semesterId);
        Task<IEnumerable<Topic>> GetTopicsBySemesterAsync(int semesterId);
        Task<IEnumerable<Topic>> GetActiveTopicsAsync();
        Task<IEnumerable<Topic>> GetTopicsByCategoryAsync(string category);
    }
}
