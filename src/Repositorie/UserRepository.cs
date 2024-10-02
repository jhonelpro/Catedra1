using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Data;
using api.src.DTOs;
using api.src.Interfaces;
using api.src.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.src.Repositorie
{
    public class UserRepository : IUserInterface
    {   
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<User?> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> existRut(string rut)
        {
            return await _context.Users.AnyAsync(x => x.Rut == rut);
        }

        public Task<List<UserDto>> GetUser()
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}