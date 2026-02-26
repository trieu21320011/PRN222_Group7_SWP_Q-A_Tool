using BussinessLayer.ViewModels.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDTO>> GetAllUsersAsync();
        Task<GetUserDTO?> GetUserByIdAsync(int id);
        Task<GetUserDTO?> GetUserByEmailAsync(string email);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<UserDTO?> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<GetUserDTO>> GetUsersByRoleAsync(int roleId);
        Task<IEnumerable<GetUserDTO>> GetActiveUsersAsync();
    }
}
