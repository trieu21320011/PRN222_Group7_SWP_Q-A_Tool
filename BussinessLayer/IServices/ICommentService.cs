using BussinessLayer.ViewModels.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<GetCommentDTO>> GetAllCommentsAsync();
        Task<GetCommentDTO?> GetCommentByIdAsync(int id);
        Task<CommentDTO> CreateCommentAsync(CreateCommentDTO createCommentDTO);
        Task<CommentDTO?> UpdateCommentAsync(UpdateCommentDTO updateCommentDTO);
        Task<bool> DeleteCommentAsync(int id);
        Task<IEnumerable<GetCommentDTO>> GetCommentsByQuestionAsync(int questionId);
        Task<IEnumerable<GetCommentDTO>> GetCommentsByAnswerAsync(int answerId);
        Task<IEnumerable<GetCommentDTO>> GetCommentsByAuthorAsync(int authorId);
    }
}
