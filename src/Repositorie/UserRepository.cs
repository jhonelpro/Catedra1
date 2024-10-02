using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Data;
using api.src.DTOs;
using api.src.Interfaces;
using api.src.Mappers;
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

        public async Task<User?> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> existRut(string rut)
        {
            return await _context.Users.AnyAsync(x => x.Rut == rut);
        }

        public async Task<List<UserDto>> GetUser()
        {
            return await _context.Users.Select(u => u.ToUserDto()).ToListAsync();
        }

        public async Task<User?> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            user.Rut = updateUserDto.Rut;
            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Gender = updateUserDto.Gender;
            user.DateOfBirth = updateUserDto.DateOfBirth;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}