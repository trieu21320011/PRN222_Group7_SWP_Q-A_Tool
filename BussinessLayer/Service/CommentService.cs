using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CommentDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetCommentDTO>> GetAllCommentsAsync()
        {
            try
            {
                var comments = await _unitOfWork.CommentRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetCommentDTO?> GetCommentByIdAsync(int id)
        {
            try
            {
                var comment = await _unitOfWork.CommentRepo.GetByIdAsync(id);
                if (comment == null)
                {
                    return null;
                }
                return _mapper.Map<GetCommentDTO>(comment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CommentDTO> CreateCommentAsync(CreateCommentDTO createCommentDTO)
        {
            try
            {
                // Create comment entity manually to avoid navigation property issues
                var comment = new Comment
                {
                    Body = createCommentDTO.Body,
                    AuthorId = createCommentDTO.AuthorId,
                    QuestionId = createCommentDTO.QuestionId,
                    AnswerId = createCommentDTO.AnswerId,
                    ParentCommentId = createCommentDTO.ParentCommentId,
                    CreatedAt = DateTime.UtcNow,
                    IsEdited = false
                };

                await _unitOfWork.CommentRepo.AddAsync(comment);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                // Update comment count after successful insert
                if (isSuccess)
                {
                    if (createCommentDTO.QuestionId.HasValue)
                    {
                        var question = await _unitOfWork.QuestionRepo.GetByIdAsync(createCommentDTO.QuestionId.Value);
                        if (question != null)
                        {
                            question.CommentCount = (question.CommentCount ?? 0) + 1;
                            question.LastActivityAt = DateTime.UtcNow;
                            await _unitOfWork.SaveChangeAsync();
                        }
                    }
                    else if (createCommentDTO.AnswerId.HasValue)
                    {
                        var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(createCommentDTO.AnswerId.Value);
                        if (answer != null)
                        {
                            answer.CommentCount = (answer.CommentCount ?? 0) + 1;
                            await _unitOfWork.SaveChangeAsync();
                        }
                    }

                    return _mapper.Map<CommentDTO>(comment);
                }
                else
                {
                    return new CommentDTO();
                }
            }
            catch (Exception ex)
            {
                // Preserve inner exception for better debugging
                throw;
            }
        }

        public async Task<CommentDTO?> UpdateCommentAsync(UpdateCommentDTO updateCommentDTO)
        {
            try
            {
                var existingComment = await _unitOfWork.CommentRepo.GetByIdAsync(updateCommentDTO.CommentId);
                if (existingComment == null)
                {
                    return null;
                }

                existingComment.Body = updateCommentDTO.Body;
                existingComment.IsEdited = true;
                existingComment.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.CommentRepo.Update(existingComment);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<CommentDTO>(existingComment);
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

        public async Task<bool> DeleteCommentAsync(int id)
        {
            try
            {
                var comment = await _unitOfWork.CommentRepo.GetByIdAsync(id);
                if (comment == null)
                {
                    return false;
                }

                // Update comment count on question or answer
                // Entity is already tracked - changes auto-detected on SaveChanges
                if (comment.QuestionId.HasValue)
                {
                    var question = await _unitOfWork.QuestionRepo.GetByIdAsync(comment.QuestionId.Value);
                    if (question != null)
                    {
                        question.CommentCount = Math.Max((question.CommentCount ?? 0) - 1, 0);
                    }
                }
                else if (comment.AnswerId.HasValue)
                {
                    var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(comment.AnswerId.Value);
                    if (answer != null)
                    {
                        answer.CommentCount = Math.Max((answer.CommentCount ?? 0) - 1, 0);
                    }
                }

                _unitOfWork.CommentRepo.Delete(comment);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCommentDTO>> GetCommentsByQuestionAsync(int questionId)
        {
            try
            {
                var comments = await _unitOfWork.CommentRepo.GetCommentsByQuestionAsync(questionId);
                return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCommentDTO>> GetCommentsByAnswerAsync(int answerId)
        {
            try
            {
                var comments = await _unitOfWork.CommentRepo.GetCommentsByAnswerAsync(answerId);
                return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCommentDTO>> GetCommentsByAuthorAsync(int authorId)
        {
            try
            {
                var comments = await _unitOfWork.CommentRepo.GetCommentsByAuthorAsync(authorId);
                return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
