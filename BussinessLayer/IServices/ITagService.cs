using BussinessLayer.ViewModels.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ITagService
    {
        Task<IEnumerable<GetTagDTO>> GetAllTagsAsync();
        Task<GetTagDTO?> GetTagByIdAsync(int id);
        Task<GetTagDTO?> GetTagBySlugAsync(string slug);
        Task<TagDTO> CreateTagAsync(CreateTagDTO createTagDTO);
        Task<TagDTO?> UpdateTagAsync(UpdateTagDTO updateTagDTO);
        Task<bool> DeleteTagAsync(int id);
        Task<IEnumerable<GetTagDTO>> GetActiveTagsAsync();
        Task<IEnumerable<GetTagDTO>> GetPopularTagsAsync(int count);
    }
}
