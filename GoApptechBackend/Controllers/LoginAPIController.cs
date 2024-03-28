using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.LoginDTO;
using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly IRepository<Person> context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public LoginAPIController(IRepository<Person> context, IMapper mapper)
        {
            this.context = context;
            this.apiResponse = new ApiResponse();
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginPersonDTO loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest("Invalid login request");
                }

                var findUser = await context.GetAsync(employee => employee.Username.ToLower() == loginRequest.Username.ToLower().Trim());

                if (findUser == null)
                {
                    return BadRequest("Invalid username or password");
                }
                
                string storedPassword = findUser.Password;

                // Verify the password using bcrypt
                bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.Password, storedPassword);

                if (!isPasswordValid)
                {
                    return BadRequest("Invalid username or password");
                }

                var personDto = mapper.Map<GetPersonDTO>(findUser);

                var response = new ApiResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = personDto
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Här kan du logga felmeddelandet för vidare felsökning
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
