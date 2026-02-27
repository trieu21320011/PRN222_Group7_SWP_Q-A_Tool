using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.QuestionDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetAllQuestionsAsync()
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetQuestionDTO?> GetQuestionByIdAsync(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepo.GetQuestionWithDetailsAsync(id);
                if (question == null)
                {
                    return null;
                }
                return _mapper.Map<GetQuestionDTO>(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuestionDTO> CreateQuestionAsync(CreateQuestionDTO createQuestionDTO)
        {
            try
            {
                var question = _mapper.Map<Question>(createQuestionDTO);
                question.CreatedAt = DateTime.UtcNow;
                question.LastActivityAt = DateTime.UtcNow;
                question.Status = "Open";
                question.ViewCount = 0;
                question.AnswerCount = 0;
                question.CommentCount = 0;
                question.IsPinned = false;
                question.IsClosed = false;
                question.IsPrivate = createQuestionDTO.IsPrivate ?? false;

                await _unitOfWork.QuestionRepo.AddAsync(question);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<QuestionDTO>(question);
                }
                else
                {
                    return new QuestionDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuestionDTO?> UpdateQuestionAsync(UpdateQuestionDTO updateQuestionDTO)
        {
            try
            {
                var existingQuestion = await _unitOfWork.QuestionRepo.GetByIdAsync(updateQuestionDTO.QuestionId);
                if (existingQuestion == null)
                {
                    return null;
                }

                existingQuestion.Title = updateQuestionDTO.Title;
                existingQuestion.Body = updateQuestionDTO.Body;
                existingQuestion.Excerpt = updateQuestionDTO.Excerpt;
                existingQuestion.TeamId = updateQuestionDTO.TeamId;
                existingQuestion.CoreId = updateQuestionDTO.CoreId;
                existingQuestion.TopicId = updateQuestionDTO.TopicId;
                existingQuestion.AssignedInstructorId = updateQuestionDTO.AssignedInstructorId;
                existingQuestion.Status = updateQuestionDTO.Status;
                existingQuestion.Category = updateQuestionDTO.Category;
                existingQuestion.Difficulty = updateQuestionDTO.Difficulty;
                existingQuestion.IsPinned = updateQuestionDTO.IsPinned;
                existingQuestion.IsPrivate = updateQuestionDTO.IsPrivate;
                existingQuestion.IsClosed = updateQuestionDTO.IsClosed;
                existingQuestion.ClosedReason = updateQuestionDTO.ClosedReason;
                existingQuestion.ClosedById = updateQuestionDTO.ClosedById;
                existingQuestion.UpdatedAt = DateTime.UtcNow;
                existingQuestion.LastActivityAt = DateTime.UtcNow;

                if (updateQuestionDTO.IsClosed == true && existingQuestion.ClosedAt == null)
                {
                    existingQuestion.ClosedAt = DateTime.UtcNow;
                }

                _unitOfWork.QuestionRepo.Update(existingQuestion);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<QuestionDTO>(existingQuestion);
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

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepo.GetByIdAsync(id);
                if (question == null)
                {
                    return false;
                }

                _unitOfWork.QuestionRepo.Delete(question);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetQuestionsByAuthorAsync(int authorId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetQuestionsByAuthorAsync(authorId);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetQuestionsByTeamAsync(int teamId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetQuestionsByTeamAsync(teamId);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetQuestionsByTopicAsync(int topicId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetQuestionsByTopicAsync(topicId);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetQuestionsByCoreAsync(int coreId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetQuestionsByCoreAsync(coreId);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetQuestionsByCategoryAsync(string category)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetQuestionsByCategoryAsync(category);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetPinnedQuestionsAsync()
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetPinnedQuestionsAsync();
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetRecentQuestionsAsync(int count)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetRecentQuestionsAsync(count);
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetQuestionDTO>> GetOpenQuestionsAsync()
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepo.GetOpenQuestionsAsync();
                return _mapper.Map<IEnumerable<GetQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
