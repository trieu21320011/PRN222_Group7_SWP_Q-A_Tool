using BussinessLayer.ViewModels.TeamDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ITeamService
    {
        Task<IEnumerable<GetTeamDTO>> GetAllTeamsAsync();
        Task<GetTeamDTO?> GetTeamByIdAsync(int id);
        Task<GetTeamDTO?> GetTeamByCodeAsync(string teamCode);
        Task<TeamDTO> CreateTeamAsync(CreateTeamDTO createTeamDTO);
        Task<TeamDTO?> UpdateTeamAsync(UpdateTeamDTO updateTeamDTO);
        Task<bool> DeleteTeamAsync(int id);
        Task<IEnumerable<GetTeamDTO>> GetTeamsByLeaderAsync(int leaderId);
        Task<IEnumerable<GetTeamDTO>> GetTeamsByMentorAsync(int mentorId);
        Task<IEnumerable<GetTeamDTO>> GetActiveTeamsAsync();
        Task<IEnumerable<GetTeamDTO>> GetTeamsBySemesterAsync(int semesterId);
        Task<IEnumerable<GetTeamDTO>> GetTeamsByCoreAsync(int coreId);
        Task<IEnumerable<GetTeamDTO>> GetTeamsByTopicAsync(int topicId);
    }
}
