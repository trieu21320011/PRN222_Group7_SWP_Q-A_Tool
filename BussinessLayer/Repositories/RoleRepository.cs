using BussinessLayer.IRepositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SWP391QAContext _context;

        public RoleRepository(SWP391QAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public Task<Role?> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}