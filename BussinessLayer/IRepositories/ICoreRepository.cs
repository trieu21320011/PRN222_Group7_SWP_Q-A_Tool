using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ICoreRepository : IGenericRepository<Core>
    {
        Task<Core?> GetCoreWithDetailsAsync(int coreId);
        Task<Core?> GetCoreByCodeAsync(string coreCode, int semesterId);
        Task<IEnumerable<Core>> GetCoresBySemesterAsync(int semesterId);
        Task<IEnumerable<Core>> GetCoresByInstructorAsync(int instructorId);
        Task<IEnumerable<Core>> GetActiveCoresAsync();
    }
}
