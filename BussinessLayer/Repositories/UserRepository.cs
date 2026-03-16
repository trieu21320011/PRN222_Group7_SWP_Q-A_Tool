using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SWP391QAContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> GetUserWithRoleAsync(int userId)
        {
            try
            {
                var result = await _dbContext.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.UserId == userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
        {
            try
            {
                var result = await _dbContext.Users
                    .Where(x => x.RoleId == roleId)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            try
            {
                var result = await _dbContext.Users
                    .Where(x => x.IsActive == true)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Email == email && x.IsActive == true);

                if (user == null)
                    return null;

                // Simple password comparison (no hashing for now)
                if (user.PasswordHash == password)
                    return user;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
