using BussinessLayer.ViewModels.CoreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface ICoreService
    {
        Task<IEnumerable<GetCoreDTO>> GetAllCoresAsync();
        Task<GetCoreDTO?> GetCoreByIdAsync(int id);
        Task<GetCoreDTO?> GetCoreByCodeAsync(string coreCode, int semesterId);
        Task<CoreDTO> CreateCoreAsync(CreateCoreDTO createCoreDTO);
        Task<CoreDTO?> UpdateCoreAsync(UpdateCoreDTO updateCoreDTO);
        Task<bool> DeleteCoreAsync(int id);
        Task<IEnumerable<GetCoreDTO>> GetCoresBySemesterAsync(int semesterId);
        Task<IEnumerable<GetCoreDTO>> GetCoresByInstructorAsync(int instructorId);
        Task<IEnumerable<GetCoreDTO>> GetActiveCoresAsync();
    }
}
