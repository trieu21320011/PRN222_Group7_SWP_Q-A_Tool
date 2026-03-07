using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class SystemInstructorRepository : GenericRepository<SystemInstructor>, ISystemInstructorRepository
    {
        public SystemInstructorRepository(SWP391QAContext dbContext) : base(dbContext) { }

        public async Task<SystemInstructor?> GetSystemInstructorWithDetailsAsync(int systemInstructorId)
        {
            return await _dbContext.SystemInstructors
                .Include(i => i.User)
                .Include(i => i.Semester)
                .FirstOrDefaultAsync(i => i.SystemInstructorId == systemInstructorId);
        }

        public async Task<IEnumerable<SystemInstructor>> GetInstructorsBySemesterAsync(int semesterId)
        {
            return await _dbContext.SystemInstructors
                .Where(i => i.SemesterId == semesterId)
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<SystemInstructor?> GetInstructorByUserAndSemesterAsync(int userId, int semesterId)
        {
            return await _dbContext.SystemInstructors
                .FirstOrDefaultAsync(i => i.UserId == userId && i.SemesterId == semesterId);
        }

        public async Task<IEnumerable<SystemInstructor>> GetHeadInstructorsAsync(int semesterId)
        {
            return await _dbContext.SystemInstructors
                .Where(i => i.SemesterId == semesterId && i.IsHead == true)
                .Include(i => i.User)
                .ToListAsync();
        }
    }
}
