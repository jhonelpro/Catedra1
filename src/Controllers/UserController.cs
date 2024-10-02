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
                return TypedResults.BadRequest(ModelState);
            }

            var _user = user.ToProductFromCreateDto();

            bool existRut = await _userRepository.existRut(_user.Rut);
            
            if (existRut)
            {
                return TypedResults.Conflict("Rut already exists");
            }

            var newUser = await _userRepository.CreateUser(_user);
            return TypedResults.Ok(newUser.ToUserDto());
        }

        [HttpGet]
        public async Task<IResult> GetUser()
        {
            var users = await _userRepository.GetUser();

            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }
            
            return TypedResults.Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateUser(int id, [FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            var updatedUser = await _userRepository.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(updatedUser.ToUserDto());
        }
    }
}