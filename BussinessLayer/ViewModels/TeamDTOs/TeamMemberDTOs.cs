using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.TeamDTOs
{
    public class TeamMemberDTO
    {
        public int TeamMemberId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? StudentId { get; set; }
        public string? Role { get; set; }
        public DateTime? JoinedAt { get; set; }
    }

    public class AddTeamMemberDTO
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
    }

    public class UpdateTeamMemberRoleDTO
    {
        public int TeamMemberId { get; set; }
        public string? Role { get; set; }
    }
}
