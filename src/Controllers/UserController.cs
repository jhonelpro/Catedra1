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
            return TypedResults.Ok(newUser);
        }
    }
}