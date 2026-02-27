using BussinessLayer.ViewModels.SemesterDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ISemesterService
    {
        Task<IEnumerable<GetSemesterDTO>> GetAllSemestersAsync();
        Task<GetSemesterDTO?> GetSemesterByIdAsync(int id);
        Task<GetSemesterDTO?> GetSemesterByCodeAsync(string semesterCode);
        Task<GetSemesterDTO?> GetCurrentSemesterAsync();
        Task<SemesterDTO> CreateSemesterAsync(CreateSemesterDTO createSemesterDTO);
        Task<SemesterDTO?> UpdateSemesterAsync(UpdateSemesterDTO updateSemesterDTO);
        Task<bool> DeleteSemesterAsync(int id);
        Task<IEnumerable<GetSemesterDTO>> GetActiveSemestersAsync();
    }
}
