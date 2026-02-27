using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.AnswerDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AnswerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetAnswerDTO>> GetAllAnswersAsync()
        {
            try
            {
                var answers = await _unitOfWork.AnswerRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetAnswerDTO>>(answers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetAnswerDTO?> GetAnswerByIdAsync(int id)
        {
            try
            {
                var answer = await _unitOfWork.AnswerRepo.GetAnswerWithDetailsAsync(id);
                if (answer == null)
                {
                    return null;
                }
                return _mapper.Map<GetAnswerDTO>(answer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AnswerDTO> CreateAnswerAsync(CreateAnswerDTO createAnswerDTO)
        {
            try
            {
                var answer = _mapper.Map<Answer>(createAnswerDTO);
                answer.CreatedAt = DateTime.UtcNow;
                answer.IsAccepted = false;
                answer.CommentCount = 0;
                answer.IsMentorAnswer = createAnswerDTO.IsMentorAnswer ?? false;

                await _unitOfWork.AnswerRepo.AddAsync(answer);

                // Update question's answer count and last activity
                var question = await _unitOfWork.QuestionRepo.GetByIdAsync(createAnswerDTO.QuestionId);
                if (question != null)
                {
                    question.AnswerCount = (question.AnswerCount ?? 0) + 1;
                    question.LastActivityAt = DateTime.UtcNow;
                    _unitOfWork.QuestionRepo.Update(question);
                }

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<AnswerDTO>(answer);
                }
                else
                {
                    return new AnswerDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AnswerDTO?> UpdateAnswerAsync(UpdateAnswerDTO updateAnswerDTO)
        {
            try
            {
                var existingAnswer = await _unitOfWork.AnswerRepo.GetByIdAsync(updateAnswerDTO.AnswerId);
                if (existingAnswer == null)
                {
                    return null;
                }

                existingAnswer.Body = updateAnswerDTO.Body;
                existingAnswer.IsAccepted = updateAnswerDTO.IsAccepted;
                existingAnswer.IsMentorAnswer = updateAnswerDTO.IsMentorAnswer;
                existingAnswer.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.AnswerRepo.Update(existingAnswer);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<AnswerDTO>(existingAnswer);
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

        public async Task<bool> DeleteAnswerAsync(int id)
        {
            try
            {
                var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(id);
                if (answer == null)
                {
                    return false;
                }

                // Update question's answer count
                var question = await _unitOfWork.QuestionRepo.GetByIdAsync(answer.QuestionId);
                if (question != null)
                {
                    question.AnswerCount = Math.Max((question.AnswerCount ?? 0) - 1, 0);
                    _unitOfWork.QuestionRepo.Update(question);
                }

                _unitOfWork.AnswerRepo.Delete(answer);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetAnswerDTO>> GetAnswersByQuestionAsync(int questionId)
        {
            try
            {
                var answers = await _unitOfWork.AnswerRepo.GetAnswersByQuestionAsync(questionId);
                return _mapper.Map<IEnumerable<GetAnswerDTO>>(answers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetAnswerDTO>> GetAnswersByAuthorAsync(int authorId)
        {
            try
            {
                var answers = await _unitOfWork.AnswerRepo.GetAnswersByAuthorAsync(authorId);
                return _mapper.Map<IEnumerable<GetAnswerDTO>>(answers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetAnswerDTO?> GetAcceptedAnswerByQuestionAsync(int questionId)
        {
            try
            {
                var answer = await _unitOfWork.AnswerRepo.GetAcceptedAnswerByQuestionAsync(questionId);
                if (answer == null)
                {
                    return null;
                }
                return _mapper.Map<GetAnswerDTO>(answer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AcceptAnswerAsync(int answerId, int questionId)
        {
            try
            {
                var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(answerId);
                if (answer == null || answer.QuestionId != questionId)
                {
                    return false;
                }

                // Unaccept any previously accepted answer
                var previouslyAccepted = await _unitOfWork.AnswerRepo.GetAcceptedAnswerByQuestionAsync(questionId);
                if (previouslyAccepted != null)
                {
                    previouslyAccepted.IsAccepted = false;
                    _unitOfWork.AnswerRepo.Update(previouslyAccepted);
                }

                // Accept the new answer
                answer.IsAccepted = true;
                _unitOfWork.AnswerRepo.Update(answer);

                // Update question's accepted answer id
                var question = await _unitOfWork.QuestionRepo.GetByIdAsync(questionId);
                if (question != null)
                {
                    question.AcceptedAnswerId = answerId;
                    question.LastActivityAt = DateTime.UtcNow;
                    _unitOfWork.QuestionRepo.Update(question);
                }

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
