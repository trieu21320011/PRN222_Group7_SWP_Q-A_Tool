using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {
        Task<TeamMember?> GetTeamMemberWithDetailsAsync(int teamMemberId);
        Task<IEnumerable<TeamMember>> GetMembersByTeamAsync(int teamId);
        Task<IEnumerable<TeamMember>> GetTeamsByUserAsync(int userId);
        Task<TeamMember?> GetMembershipAsync(int teamId, int userId);
    }
}
