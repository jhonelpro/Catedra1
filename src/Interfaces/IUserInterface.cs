using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.DTOs;
using api.src.Models;

namespace api.src.Interfaces
{
    public interface IUserInterface
    {
        Task<bool> existRut(string rut);
        Task<User> CreateUser(User user);
        Task<List<UserDto>> GetUser();
        Task<User?> UpdateUser(int id, UpdateUserDto updateUserDto);
        Task<User?> DeleteUser(int id);
    }
}