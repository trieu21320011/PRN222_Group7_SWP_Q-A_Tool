using BussinessLayer.ViewModels.QuestionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface IQuestionService
    {
        Task<IEnumerable<GetQuestionDTO>> GetAllQuestionsAsync();
        Task<GetQuestionDTO?> GetQuestionByIdAsync(int id);
        Task<QuestionDTO> CreateQuestionAsync(CreateQuestionDTO createQuestionDTO);
        Task<QuestionDTO?> UpdateQuestionAsync(UpdateQuestionDTO updateQuestionDTO);
        Task<bool> DeleteQuestionAsync(int id);
        Task<IEnumerable<GetQuestionDTO>> GetQuestionsByAuthorAsync(int authorId);
        Task<IEnumerable<GetQuestionDTO>> GetQuestionsByTeamAsync(int teamId);
        Task<IEnumerable<GetQuestionDTO>> GetQuestionsByTagAsync(int tagId);
        Task<IEnumerable<GetQuestionDTO>> GetPinnedQuestionsAsync();
        Task<IEnumerable<GetQuestionDTO>> GetRecentQuestionsAsync(int count);
    }
}
