using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag?> GetTagBySlugAsync(string slug);
        Task<Tag?> GetTagByNameAsync(string tagName);
        Task<IEnumerable<Tag>> GetActiveTagsAsync();
        Task<IEnumerable<Tag>> GetPopularTagsAsync(int count);
    }
}
