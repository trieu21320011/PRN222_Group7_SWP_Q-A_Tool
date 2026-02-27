using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.SemesterDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class SemesterService : ISemesterService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SemesterService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetSemesterDTO>> GetAllSemestersAsync()
        {
            try
            {
                var semesters = await _unitOfWork.SemesterRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetSemesterDTO>>(semesters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetSemesterDTO?> GetSemesterByIdAsync(int id)
        {
            try
            {
                var semester = await _unitOfWork.SemesterRepo.GetByIdAsync(id);
                if (semester == null)
                {
                    return null;
                }
                return _mapper.Map<GetSemesterDTO>(semester);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetSemesterDTO?> GetSemesterByCodeAsync(string semesterCode)
        {
            try
            {
                var semester = await _unitOfWork.SemesterRepo.GetSemesterByCodeAsync(semesterCode);
                if (semester == null)
                {
                    return null;
                }
                return _mapper.Map<GetSemesterDTO>(semester);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetSemesterDTO?> GetCurrentSemesterAsync()
        {
            try
            {
                var semester = await _unitOfWork.SemesterRepo.GetCurrentSemesterAsync();
                if (semester == null)
                {
                    return null;
                }
                return _mapper.Map<GetSemesterDTO>(semester);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SemesterDTO> CreateSemesterAsync(CreateSemesterDTO createSemesterDTO)
        {
            try
            {
                var semester = _mapper.Map<Semester>(createSemesterDTO);
                semester.CreatedAt = DateTime.UtcNow;
                semester.IsActive = createSemesterDTO.IsActive ?? true;
                semester.IsCurrent = createSemesterDTO.IsCurrent ?? false;

                await _unitOfWork.SemesterRepo.AddAsync(semester);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<SemesterDTO>(semester);
                }
                else
                {
                    return new SemesterDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SemesterDTO?> UpdateSemesterAsync(UpdateSemesterDTO updateSemesterDTO)
        {
            try
            {
                var existingSemester = await _unitOfWork.SemesterRepo.GetByIdAsync(updateSemesterDTO.SemesterId);
                if (existingSemester == null)
                {
                    return null;
                }

                existingSemester.SemesterCode = updateSemesterDTO.SemesterCode;
                existingSemester.SemesterName = updateSemesterDTO.SemesterName;
                existingSemester.StartDate = updateSemesterDTO.StartDate;
                existingSemester.EndDate = updateSemesterDTO.EndDate;
                existingSemester.IsActive = updateSemesterDTO.IsActive;
                existingSemester.IsCurrent = updateSemesterDTO.IsCurrent;
                existingSemester.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.SemesterRepo.Update(existingSemester);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<SemesterDTO>(existingSemester);
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

        public async Task<bool> DeleteSemesterAsync(int id)
        {
            try
            {
                var semester = await _unitOfWork.SemesterRepo.GetByIdAsync(id);
                if (semester == null)
                {
                    return false;
                }

                _unitOfWork.SemesterRepo.Delete(semester);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetSemesterDTO>> GetActiveSemestersAsync()
        {
            try
            {
                var semesters = await _unitOfWork.SemesterRepo.GetActiveSemestersAsync();
                return _mapper.Map<IEnumerable<GetSemesterDTO>>(semesters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
