using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models.DTO.PersonDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/QuizResult")]
    [ApiController]
    public class EmpQuizResultController : ControllerBase
    {
        private readonly ApplicationContext context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public EmpQuizResultController(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.apiResponse = new ApiResponse();
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetResults()
        {
            try
            {

                var resultsWithPersonAndQuiz = await context.EmployeeResults
                    .Include(result => result.Persons)
                    .Include(result => result.Quizzes)
                    .ToListAsync();

                var mappedResult = resultsWithPersonAndQuiz.Select(result =>
                    new EmployeeResultDTO
                    {
                        Username = result.Persons?.Username,
                        QuizHeading = result.Quizzes?.QuizHeading,
                        GuessedAnswer = result.GuessedAnswer,
                        isCorrect = result.isCorrect ? "Correct answer" : "Incorrect answer",
                        QuizDate = result.QuizDate.ToString()
                    }).ToList();

                var apiResponse = new ApiResponse
                {
                    Result = mappedResult,
                    StatusCode = HttpStatusCode.OK,
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

        [HttpGet("{id:int}", Name = "GetQuizResults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetResultsById(int id)
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
                        Errors = new List<string>() { "Person not found" }
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
