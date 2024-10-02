using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.DTOs;
using api.src.Models;

namespace api.src.Mappers
{
    public static class UserMapper
    {
        public static User ToProductFromCreateDto(this CreateUserDto userDto)
        {
            return new User
            {
                Rut = userDto.Rut,
                Name = userDto.Name,
                Email = userDto.Email,
                Gender = userDto.Gender,
                DateOfBirth = userDto.DateOfBirth
            };
        }
    }
}