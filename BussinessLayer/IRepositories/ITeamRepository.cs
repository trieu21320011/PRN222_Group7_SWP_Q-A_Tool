using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<Team?> GetTeamWithDetailsAsync(int teamId);
        Task<Team?> GetTeamByCodeAsync(string teamCode);
        Task<IEnumerable<Team>> GetTeamsByLeaderAsync(int leaderId);
        Task<IEnumerable<Team>> GetTeamsByMentorAsync(int mentorId);
        Task<IEnumerable<Team>> GetActiveTeamsAsync();
        Task<IEnumerable<Team>> GetTeamsBySemesterAsync(int semesterId);
        Task<IEnumerable<Team>> GetTeamsByCoreAsync(int coreId);
        Task<IEnumerable<Team>> GetTeamsByTopicAsync(int topicId);
    }
}
