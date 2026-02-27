using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TopicDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class TopicService : ITopicService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetTopicDTO>> GetAllTopicsAsync()
        {
            try
            {
                var topics = await _unitOfWork.TopicRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetTopicDTO>>(topics);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTopicDTO?> GetTopicByIdAsync(int id)
        {
            try
            {
                var topic = await _unitOfWork.TopicRepo.GetTopicWithDetailsAsync(id);
                if (topic == null)
                {
                    return null;
                }
                return _mapper.Map<GetTopicDTO>(topic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTopicDTO?> GetTopicByCodeAsync(string topicCode, int semesterId)
        {
            try
            {
                var topic = await _unitOfWork.TopicRepo.GetTopicByCodeAsync(topicCode, semesterId);
                if (topic == null)
                {
                    return null;
                }
                return _mapper.Map<GetTopicDTO>(topic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TopicDTO> CreateTopicAsync(CreateTopicDTO createTopicDTO)
        {
            try
            {
                var topic = _mapper.Map<Topic>(createTopicDTO);
                topic.CreatedAt = DateTime.UtcNow;
                topic.CurrentTeams = 0;
                topic.IsActive = createTopicDTO.IsActive ?? true;

                await _unitOfWork.TopicRepo.AddAsync(topic);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<TopicDTO>(topic);
                }
                else
                {
                    return new TopicDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TopicDTO?> UpdateTopicAsync(UpdateTopicDTO updateTopicDTO)
        {
            try
            {
                var existingTopic = await _unitOfWork.TopicRepo.GetByIdAsync(updateTopicDTO.TopicId);
                if (existingTopic == null)
                {
                    return null;
                }

                existingTopic.TopicCode = updateTopicDTO.TopicCode;
                existingTopic.TopicName = updateTopicDTO.TopicName;
                existingTopic.Description = updateTopicDTO.Description;
                existingTopic.Category = updateTopicDTO.Category;
                existingTopic.Difficulty = updateTopicDTO.Difficulty;
                existingTopic.MaxTeams = updateTopicDTO.MaxTeams;
                existingTopic.IsActive = updateTopicDTO.IsActive;
                existingTopic.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.TopicRepo.Update(existingTopic);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<TopicDTO>(existingTopic);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTopicAsync(int id)
        {
            try
            {
                var topic = await _unitOfWork.TopicRepo.GetByIdAsync(id);
                if (topic == null)
                {
                    return false;
                }

                _unitOfWork.TopicRepo.Delete(topic);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTopicDTO>> GetTopicsBySemesterAsync(int semesterId)
        {
            try
            {
                var topics = await _unitOfWork.TopicRepo.GetTopicsBySemesterAsync(semesterId);
                return _mapper.Map<IEnumerable<GetTopicDTO>>(topics);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTopicDTO>> GetActiveTopicsAsync()
        {
            try
            {
                var topics = await _unitOfWork.TopicRepo.GetActiveTopicsAsync();
                return _mapper.Map<IEnumerable<GetTopicDTO>>(topics);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTopicDTO>> GetTopicsByCategoryAsync(string category)
        {
            try
            {
                var topics = await _unitOfWork.TopicRepo.GetTopicsByCategoryAsync(category);
                return _mapper.Map<IEnumerable<GetTopicDTO>>(topics);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
