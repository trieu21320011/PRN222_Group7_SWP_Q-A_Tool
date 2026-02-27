using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IRepositories
{
    public interface ISystemInstructorRepository : IGenericRepository<SystemInstructor>
    {
        Task<SystemInstructor?> GetSystemInstructorWithDetailsAsync(int systemInstructorId);
        Task<IEnumerable<SystemInstructor>> GetInstructorsBySemesterAsync(int semesterId);
        Task<SystemInstructor?> GetInstructorByUserAndSemesterAsync(int userId, int semesterId);
        Task<IEnumerable<SystemInstructor>> GetHeadInstructorsAsync(int semesterId);
    }
}
