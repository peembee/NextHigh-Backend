using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.ModifiedDTOs;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/PongRanks")]
    [ApiController]
    public class PingPongRankAPIController : ControllerBase
    {

        private readonly ApplicationContext context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public PingPongRankAPIController(ApplicationContext context, IMapper mapper)
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
                var personListWithRankNames = await context.Persons.Include(tabel => tabel.PingPongRanks).ToListAsync();

                var mappedResult = personListWithRankNames.Select(person => new PersonWithRankDTO
                {
                    Username = person.Username,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    RankTitle = person.PingPongRanks.RankTitle ?? "Unknown"
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

        [HttpGet("{id:int}", Name = "GetPongRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetRank(int id)
        {
            try
            {
                var personWithRank = await context.Persons
                 .Include(table => table.PingPongRanks)
                 .Where(person => person.PersonID == id)
                 .FirstOrDefaultAsync();

                if (personWithRank != null)
                {
                    var mappedResult = new PersonWithRankDTO
                    {
                        Username = personWithRank.Username,
                        FirstName = personWithRank.FirstName,
                        LastName = personWithRank.LastName,
                        RankTitle = personWithRank.PingPongRanks?.RankTitle ?? "Unknown"
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
                        IsSuccess = false,
                        Errors = new List<string>() { "not found" }
                    };

                    return NotFound(apiResponse);
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
