using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.DTOs;
using api.src.Interfaces;
using api.src.Mappers;
using api.src.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.src.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userRepository;

        public UserController(IUserInterface userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        public async Task<IResult> AddUser([FromBody] CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest("Alguna validación no fue cumplida.");
            }

            // Validar que la fecha de nacimiento sea menor a la fecha actual
            if (user.DateOfBirth >= DateOnly.FromDateTime(DateTime.Today))
            {
                return TypedResults.BadRequest("La fecha de nacimiento debe ser menor a la fecha actual.");
            }

            var _user = user.ToProductFromCreateDto();

            bool existRut = await _userRepository.existRut(_user.Rut);
            
            if (existRut)
            {
                return TypedResults.Conflict("Rut already exists");
            }

            var newUser = await _userRepository.CreateUser(_user);
            return TypedResults.Created("Usuario creado exitosamente.", newUser.ToUserDto());
        }

        [HttpGet]
        public async Task<IResult> GetUser([FromQuery] string? gender, [FromQuery] string? sort)
        {
            var users = await _userRepository.GetUser(gender,sort);

            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest("Algún filtro es inválido.");
            }
            
            return TypedResults.Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateUser(int id, [FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest("Alguna validación no fue cumplida.");
            }

            if (user.DateOfBirth >= DateOnly.FromDateTime(DateTime.Today))
            {
                return TypedResults.BadRequest("La fecha de nacimiento debe ser menor a la fecha actual.");
            }

            var updatedUser = await _userRepository.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return TypedResults.NotFound("Usuario no encontrado.");
            }

            return TypedResults.Ok(updatedUser.ToUserDto());
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest("Alguna validación no fue cumplida.");
            }

            var deletedUser = await _userRepository.DeleteUser(id);

            if (deletedUser == null)
            {
                return TypedResults.NotFound("Usuario no encontrado.");
            }

            return TypedResults.Ok(deletedUser.ToUserDto());
        }
    }
}