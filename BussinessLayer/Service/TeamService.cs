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
    }
}
