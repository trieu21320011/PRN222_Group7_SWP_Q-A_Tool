using BussinessLayer.ViewModels.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface IAnswerService
    {
        Task<IEnumerable<GetAnswerDTO>> GetAllAnswersAsync();
        Task<GetAnswerDTO?> GetAnswerByIdAsync(int id);
        Task<AnswerDTO> CreateAnswerAsync(CreateAnswerDTO createAnswerDTO);
        Task<AnswerDTO?> UpdateAnswerAsync(UpdateAnswerDTO updateAnswerDTO);
        Task<bool> DeleteAnswerAsync(int id);
        Task<IEnumerable<GetAnswerDTO>> GetAnswersByQuestionAsync(int questionId);
        Task<IEnumerable<GetAnswerDTO>> GetAnswersByAuthorAsync(int authorId);
        Task<GetAnswerDTO?> GetAcceptedAnswerByQuestionAsync(int questionId);
        Task<bool> AcceptAnswerAsync(int answerId, int questionId);
    }
}
