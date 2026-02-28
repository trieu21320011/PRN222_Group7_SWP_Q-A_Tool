using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class CoreRepository : GenericRepository<Core>, ICoreRepository
    {
        public CoreRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Core?> GetCoreWithDetailsAsync(int coreId)
        {
            return await _dbContext.Cores
                .Include(x => x.Instructor)
                .Include(x => x.Semester)
                .Include(x => x.Teams)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.CoreId == coreId);
        }

        public async Task<Core?> GetCoreByCodeAsync(string coreCode, int semesterId)
        {
            return await _dbContext.Cores
                .Include(x => x.Instructor)
                .Include(x => x.Semester)
                .FirstOrDefaultAsync(x => x.CoreCode == coreCode
                                       && x.SemesterId == semesterId);
        }

        public async Task<IEnumerable<Core>> GetCoresBySemesterAsync(int semesterId)
        {
            return await _dbContext.Cores
                .Include(x => x.Instructor)
                .Include(x => x.Semester)
                .Where(x => x.SemesterId == semesterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Core>> GetCoresByInstructorAsync(int instructorId)
        {
            return await _dbContext.Cores
                .Include(x => x.Instructor)
                .Include(x => x.Semester)
                .Where(x => x.InstructorId == instructorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Core>> GetActiveCoresAsync()
        {
            return await _dbContext.Cores
                .Include(x => x.Instructor)
                .Include(x => x.Semester)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }
    }
}