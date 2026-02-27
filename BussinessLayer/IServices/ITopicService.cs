using BussinessLayer.ViewModels.TopicDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ITopicService
    {
        Task<IEnumerable<GetTopicDTO>> GetAllTopicsAsync();
        Task<GetTopicDTO?> GetTopicByIdAsync(int id);
        Task<GetTopicDTO?> GetTopicByCodeAsync(string topicCode, int semesterId);
        Task<TopicDTO> CreateTopicAsync(CreateTopicDTO createTopicDTO);
        Task<TopicDTO?> UpdateTopicAsync(UpdateTopicDTO updateTopicDTO);
        Task<bool> DeleteTopicAsync(int id);
        Task<IEnumerable<GetTopicDTO>> GetTopicsBySemesterAsync(int semesterId);
        Task<IEnumerable<GetTopicDTO>> GetActiveTopicsAsync();
        Task<IEnumerable<GetTopicDTO>> GetTopicsByCategoryAsync(string category);
    }
}
