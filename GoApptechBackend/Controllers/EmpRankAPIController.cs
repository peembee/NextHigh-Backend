using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models.DTO.ModifiedDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/EmployeeRanks")]
    [ApiController]
    public class EmpRankAPIController : ControllerBase
    {
        private readonly ApplicationContext context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public EmpRankAPIController(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.apiResponse = new ApiResponse();
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetRanks()
        {
            try
            {
                // Hämta alla personer från databasen tillsammans med deras respektive pingpong-ranknamn
                var personListWithRankNames = await context.Persons.Include(tabel => tabel.EmployeeRanks).ToListAsync();

                var mappedResult = personListWithRankNames.Select(person => new PersonWithEmpRankDTO
                {
                    Username = person.Username,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    RankTitle = person.EmployeeRanks.RankTitle ?? "Unknown"
                }).ToList();

                var apiResponse = new ApiResponse
                {
                    Result = mappedResult,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                var apiResponse = new ApiResponse
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToString() }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpGet("{id:int}", Name = "GetEmpRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetRank(int id)
        {
            try
            {
                var personWithRank = await context.Persons
                 .Include(table => table.EmployeeRanks)
                 .Where(person => person.PersonID == id)
                 .FirstOrDefaultAsync();

                if (personWithRank != null)
                {
                    var mappedResult = new PersonWithEmpRankDTO
                    {
                        Username = personWithRank.Username,
                        FirstName = personWithRank.FirstName,
                        LastName = personWithRank.LastName,
                        RankTitle = personWithRank.EmployeeRanks?.RankTitle ?? "Unknown"
                    };

                    var apiResponse = new ApiResponse
                    {
                        Result = mappedResult,
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true
                    };

                    return Ok(apiResponse);
                }
                else
                {
                    var apiResponse = new ApiResponse
                    {
                        Result = new List<PersonWithEmpRankDTO>(),
                        IsSuccess = true,
                        Errors = new List<string>() { "Person not found" }
                    };

                    return Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                var apiResponse = new ApiResponse
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToString() }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }
    }
}
