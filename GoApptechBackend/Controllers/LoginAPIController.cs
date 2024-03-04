using GoApptechBackend.APIResponse;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly IRepository<Person> context;
        protected ApiResponse apiResponse;

        public LoginAPIController(IRepository<Person> context)
        {
            this.context = context;
            this.apiResponse = new ApiResponse();
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

                var findUser = await context.GetAsync(ap => ap.Username.ToLower() == loginRequest.Username.ToLower().Trim());

                if (findUser == null)
                {
                    return BadRequest("Invalid username or password");
                }

                var response = new ApiResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    ApiResponseWithID = findUser.PersonID
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
