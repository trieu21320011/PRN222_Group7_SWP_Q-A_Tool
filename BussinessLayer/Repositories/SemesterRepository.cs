using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repositories
{
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<Semester?> GetSemesterByCodeAsync(string semesterCode)
        {
            try
            {
                return await _dbContext.Semesters
                    .FirstOrDefaultAsync(x => x.SemesterCode == semesterCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Semester?> GetCurrentSemesterAsync()
        {
            try
            {
                return await _dbContext.Semesters
                    .FirstOrDefaultAsync(x => x.IsCurrent == true && x.IsActive == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Semester>> GetActiveSemestersAsync()
        {
            try
            {
                return await _dbContext.Semesters
                    .Where(x => x.IsActive == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}