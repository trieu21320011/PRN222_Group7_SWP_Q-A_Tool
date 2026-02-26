using AutoMapper;
using BussinessLayer.IServices;
using BussinessLayer.ViewModels.UserDTOs;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetUserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _unitOfWork.UserRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<GetUserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUserDTO?> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetUserWithRoleAsync(id);
                if (user == null)
                {
                    return null;
                }
                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUserDTO?> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return null;
                }
                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = _mapper.Map<User>(createUserDTO);
                user.CreatedAt = DateTime.UtcNow;
                user.ReputationPoints = 0;
                user.IsActive = createUserDTO.IsActive ?? true;
                user.IsEmailVerified = createUserDTO.IsEmailVerified ?? false;

                await _unitOfWork.UserRepo.AddAsync(user);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                else
                {
                    return new UserDTO();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO?> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            try
            {
                var existingUser = await _unitOfWork.UserRepo.GetByIdAsync(updateUserDTO.UserId);
                if (existingUser == null)
                {
                    return null;
                }

                // Update properties
                existingUser.Email = updateUserDTO.Email;
                existingUser.FullName = updateUserDTO.FullName;
                existingUser.DisplayName = updateUserDTO.DisplayName;
                existingUser.AvatarUrl = updateUserDTO.AvatarUrl;
                existingUser.Bio = updateUserDTO.Bio;
                existingUser.StudentId = updateUserDTO.StudentId;
                existingUser.RoleId = updateUserDTO.RoleId;
                existingUser.IsActive = updateUserDTO.IsActive;
                existingUser.IsEmailVerified = updateUserDTO.IsEmailVerified;
                existingUser.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.UserRepo.Update(existingUser);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    return _mapper.Map<UserDTO>(existingUser);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                _unitOfWork.UserRepo.Delete(user);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetUserDTO>> GetUsersByRoleAsync(int roleId)
        {
            try
            {
                var users = await _unitOfWork.UserRepo.GetUsersByRoleAsync(roleId);
                return _mapper.Map<IEnumerable<GetUserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetUserDTO>> GetActiveUsersAsync()
        {
            try
            {
                var users = await _unitOfWork.UserRepo.GetActiveUsersAsync();
                return _mapper.Map<IEnumerable<GetUserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
