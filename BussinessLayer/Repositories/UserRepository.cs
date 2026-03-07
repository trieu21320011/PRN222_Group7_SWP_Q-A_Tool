using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
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

        public async Task<IEnumerable<User>> GetUsersWithRoleAsync()
        {
            try
            {
                var result = await _dbContext.Users
                    .Include(x => x.Role)
                    .ToListAsync();
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

                // Verify password: support both BCrypt hashed and legacy plain text
                bool passwordValid = false;
                try
                {
                    passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                }
                catch
                {
                    // Legacy: plain text password in DB (before hashing was implemented)
                    passwordValid = user.PasswordHash == password;
                }

                if (passwordValid)
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
