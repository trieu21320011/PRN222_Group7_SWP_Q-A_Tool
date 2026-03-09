using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.TeamDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetTeamDTO>> GetAllTeamsAsync()
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTeamDTO?> GetTeamByIdAsync(int id)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetTeamWithDetailsAsync(id);
                if (team == null)
                {
                    return null;
                }
                return _mapper.Map<GetTeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTeamDTO?> GetTeamByCodeAsync(string teamCode)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetTeamByCodeAsync(teamCode);
                if (team == null)
                {
                    return null;
                }
                return _mapper.Map<GetTeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TeamDTO> CreateTeamAsync(CreateTeamDTO createTeamDTO)
        {
            try
            {
                var team = _mapper.Map<Team>(createTeamDTO);
                team.CreatedAt = DateTime.UtcNow;
                team.IsActive = createTeamDTO.IsActive ?? true;

                await _unitOfWork.TeamRepo.AddAsync(team);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<TeamDTO>(team);
                }
                else
                {
                    return new TeamDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TeamDTO?> UpdateTeamAsync(UpdateTeamDTO updateTeamDTO)
        {
            try
            {
                var existingTeam = await _unitOfWork.TeamRepo.GetByIdAsync(updateTeamDTO.TeamId);
                if (existingTeam == null)
                {
                    return null;
                }

                existingTeam.TeamName = updateTeamDTO.TeamName;
                existingTeam.TeamCode = updateTeamDTO.TeamCode;
                existingTeam.Description = updateTeamDTO.Description;
                existingTeam.ProjectName = updateTeamDTO.ProjectName;
                existingTeam.LeaderId = updateTeamDTO.LeaderId;
                existingTeam.MentorId = updateTeamDTO.MentorId;
                existingTeam.CoreId = updateTeamDTO.CoreId;
                existingTeam.TopicId = updateTeamDTO.TopicId;
                existingTeam.SemesterId = updateTeamDTO.SemesterId;
                existingTeam.Semester = updateTeamDTO.Semester;
                existingTeam.IsActive = updateTeamDTO.IsActive;
                existingTeam.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.TeamRepo.Update(existingTeam);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<TeamDTO>(existingTeam);
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

        public async Task<bool> DeleteTeamAsync(int id)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetByIdAsync(id);
                if (team == null)
                {
                    return false;
                }

                _unitOfWork.TeamRepo.Delete(team);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsByLeaderAsync(int leaderId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsByLeaderAsync(leaderId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsByMentorAsync(int mentorId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsByMentorAsync(mentorId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetActiveTeamsAsync()
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetActiveTeamsAsync();
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsBySemesterAsync(int semesterId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsBySemesterAsync(semesterId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsByCoreAsync(int coreId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsByCoreAsync(coreId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsByTopicAsync(int topicId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsByTopicAsync(topicId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTeamDTO?> GetTeamWithMembersAsync(int teamId)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetTeamWithMembersAsync(teamId);
                if (team == null)
                {
                    return null;
                }
                return _mapper.Map<GetTeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTeamDTO?> GetTeamProgressAsync(int teamId)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetTeamProgressAsync(teamId);
                if (team == null)
                {
                    return null;
                }
                return _mapper.Map<GetTeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetTeamDTO>> GetTeamsByUserAsync(int userId)
        {
            try
            {
                var teams = await _unitOfWork.TeamRepo.GetTeamsByUserAsync(userId);
                return _mapper.Map<IEnumerable<GetTeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TeamDTO?> UpdateTeamNameAsync(int teamId, string newName, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newName))
                {
                    throw new Exception("Team name cannot be empty.");
                }

                var team = await _unitOfWork.TeamRepo.GetByIdAsync(teamId);
                if (team == null)
                {
                    return null;
                }

                // Check permission: only team leader can update name
                if (team.LeaderId != userId)
                {
                    throw new Exception("Only team leader can update team name.");
                }

                // Check for duplicate name in same core and semester
                if (team.CoreId.HasValue)
                {
                    var existingTeam = await _unitOfWork.TeamRepo.GetAllAsync();
                    var isDuplicate = existingTeam.Any(t => 
                        t.TeamId != teamId && 
                        t.TeamName == newName && 
                        t.CoreId == team.CoreId &&
                        t.SemesterId == team.SemesterId);

                    if (isDuplicate)
                    {
                        throw new Exception("A team with this name already exists in this course and semester.");
                    }
                }

                team.TeamName = newName;
                team.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.TeamRepo.Update(team);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<TeamDTO>(team);
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

        public async Task<IEnumerable<TeamMemberDTO>> GetTeamMembersAsync(int teamId)
        {
            try
            {
                var members = await _unitOfWork.TeamMemberRepo.GetMembersByTeamAsync(teamId);
                return _mapper.Map<IEnumerable<TeamMemberDTO>>(members);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TeamMemberDTO?> AddMemberToTeamAsync(AddTeamMemberDTO addMemberDTO, int leaderUserId)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetByIdAsync(addMemberDTO.TeamId);
                if (team == null)
                {
                    throw new Exception("Team not found.");
                }

                // Check permission: only team leader can add members
                if (team.LeaderId != leaderUserId)
                {
                    throw new Exception("Only team leader can add members.");
                }

                // Check if user exists
                var user = await _unitOfWork.UserRepo.GetByIdAsync(addMemberDTO.UserId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                // Check if user is already a member
                var existingMembership = await _unitOfWork.TeamMemberRepo.GetMembershipAsync(addMemberDTO.TeamId, addMemberDTO.UserId);
                if (existingMembership != null)
                {
                    throw new Exception("User is already a member of this team.");
                }

                // Check team capacity
                if (team.CoreId.HasValue)
                {
                    var core = await _unitOfWork.CoreRepo.GetByIdAsync(team.CoreId.Value);
                    if (core != null && core.MaxTeams.HasValue && core.CurrentTeams.HasValue)
                    {
                        var teamMemberCount = await _unitOfWork.TeamMemberRepo.GetMembersByTeamAsync(addMemberDTO.TeamId);
                        // Check if this is a reasonable capacity limit (e.g., max 10 members per team)
                        if (teamMemberCount.Count() >= 10)
                        {
                            throw new Exception("Team has reached maximum member capacity.");
                        }
                    }
                }

                var teamMember = new TeamMember
                {
                    TeamId = addMemberDTO.TeamId,
                    UserId = addMemberDTO.UserId,
                    Role = addMemberDTO.Role ?? "Member",
                    JoinedAt = DateTime.UtcNow
                };

                await _unitOfWork.TeamMemberRepo.AddAsync(teamMember);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    var addedMember = await _unitOfWork.TeamMemberRepo.GetTeamMemberWithDetailsAsync(teamMember.TeamMemberId);
                    return _mapper.Map<TeamMemberDTO>(addedMember);
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

        public async Task<bool> RemoveMemberFromTeamAsync(int teamId, int userIdToRemove, int requesterId)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetByIdAsync(teamId);
                if (team == null)
                {
                    throw new Exception("Team not found.");
                }

                // Check permission: only team leader can remove members
                if (team.LeaderId != requesterId)
                {
                    throw new Exception("Only team leader can remove members.");
                }

                var membership = await _unitOfWork.TeamMemberRepo.GetMembershipAsync(teamId, userIdToRemove);
                if (membership == null)
                {
                    throw new Exception("User is not a member of this team.");
                }

                // Check if this is the last member
                var allMembers = await _unitOfWork.TeamMemberRepo.GetMembersByTeamAsync(teamId);
                if (allMembers.Count() == 1)
                {
                    throw new Exception("Cannot remove the last member of a team.");
                }

                // Cannot remove the team leader
                if (team.LeaderId == userIdToRemove)
                {
                    throw new Exception("Cannot remove the team leader. Please reassign leadership first.");
                }

                _unitOfWork.TeamMemberRepo.Delete(membership);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> LeaveTeamAsync(int teamId, int userId)
        {
            try
            {
                var team = await _unitOfWork.TeamRepo.GetByIdAsync(teamId);
                if (team == null)
                {
                    throw new Exception("Team not found.");
                }

                // Check if user is a member
                var membership = await _unitOfWork.TeamMemberRepo.GetMembershipAsync(teamId, userId);
                if (membership == null)
                {
                    throw new Exception("User is not a member of this team.");
                }

                // Cannot leave if last member
                var allMembers = await _unitOfWork.TeamMemberRepo.GetMembersByTeamAsync(teamId);
                if (allMembers.Count() == 1)
                {
                    throw new Exception("Cannot leave a team as the last member.");
                }

                // Cannot leave if team leader (unless there are other members)
                if (team.LeaderId == userId && allMembers.Count() == 1)
                {
                    throw new Exception("The team leader cannot leave if they are the only member.");
                }

                _unitOfWork.TeamMemberRepo.Delete(membership);
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
