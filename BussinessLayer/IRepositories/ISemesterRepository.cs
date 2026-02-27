using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ISemesterRepository : IGenericRepository<Semester>
    {
        Task<Semester?> GetSemesterByCodeAsync(string semesterCode);
        Task<Semester?> GetCurrentSemesterAsync();
        Task<IEnumerable<Semester>> GetActiveSemestersAsync();
    }
}
