using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.src.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> Create([FromBody] User user)
        {
            return TypedResults.Ok();
        }
    }
}