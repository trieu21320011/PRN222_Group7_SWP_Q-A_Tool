using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.CoreDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class CoreService : ICoreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CoreService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetCoreDTO>> GetAllCoresAsync()
        {
            try
            {
                var cores = await _unitOfWork.CoreRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetCoreDTO>>(cores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetCoreDTO?> GetCoreByIdAsync(int id)
        {
            try
            {
                var core = await _unitOfWork.CoreRepo.GetCoreWithDetailsAsync(id);
                if (core == null)
                {
                    return null;
                }
                return _mapper.Map<GetCoreDTO>(core);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetCoreDTO?> GetCoreByCodeAsync(string coreCode, int semesterId)
        {
            try
            {
                var core = await _unitOfWork.CoreRepo.GetCoreByCodeAsync(coreCode, semesterId);
                if (core == null)
                {
                    return null;
                }
                return _mapper.Map<GetCoreDTO>(core);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CoreDTO> CreateCoreAsync(CreateCoreDTO createCoreDTO)
        {
            try
            {
                var core = _mapper.Map<Core>(createCoreDTO);
                core.CreatedAt = DateTime.UtcNow;
                core.CurrentTeams = 0;
                core.IsActive = createCoreDTO.IsActive ?? true;

                await _unitOfWork.CoreRepo.AddAsync(core);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<CoreDTO>(core);
                }
                else
                {
                    return new CoreDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CoreDTO?> UpdateCoreAsync(UpdateCoreDTO updateCoreDTO)
        {
            try
            {
                var existingCore = await _unitOfWork.CoreRepo.GetByIdAsync(updateCoreDTO.CoreId);
                if (existingCore == null)
                {
                    return null;
                }

                existingCore.CoreCode = updateCoreDTO.CoreCode;
                existingCore.CoreName = updateCoreDTO.CoreName;
                existingCore.InstructorId = updateCoreDTO.InstructorId;
                existingCore.MaxTeams = updateCoreDTO.MaxTeams;
                existingCore.Schedule = updateCoreDTO.Schedule;
                existingCore.Room = updateCoreDTO.Room;
                existingCore.IsActive = updateCoreDTO.IsActive;
                existingCore.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.CoreRepo.Update(existingCore);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<CoreDTO>(existingCore);
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

        public async Task<bool> DeleteCoreAsync(int id)
        {
            try
            {
                var core = await _unitOfWork.CoreRepo.GetByIdAsync(id);
                if (core == null)
                {
                    return false;
                }

                _unitOfWork.CoreRepo.Delete(core);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCoreDTO>> GetCoresBySemesterAsync(int semesterId)
        {
            try
            {
                var cores = await _unitOfWork.CoreRepo.GetCoresBySemesterAsync(semesterId);
                return _mapper.Map<IEnumerable<GetCoreDTO>>(cores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCoreDTO>> GetCoresByInstructorAsync(int instructorId)
        {
            try
            {
                var cores = await _unitOfWork.CoreRepo.GetCoresByInstructorAsync(instructorId);
                return _mapper.Map<IEnumerable<GetCoreDTO>>(cores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCoreDTO>> GetActiveCoresAsync()
        {
            try
            {
                var cores = await _unitOfWork.CoreRepo.GetActiveCoresAsync();
                return _mapper.Map<IEnumerable<GetCoreDTO>>(cores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
