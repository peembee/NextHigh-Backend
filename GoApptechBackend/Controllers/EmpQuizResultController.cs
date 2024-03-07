using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.EmployeeResultDTO;
using GoApptechBackend.Models.DTO.ModifiedDTOs;
using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Models.DTO.PingPongResultDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
                        QuizDate = result.QuizDate.ToString(),
                        Points = result.Quizzes.Points
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


        [HttpGet("{employeeId:int}", Name = "GetQuizResults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetResultsById(int employeeId)
        {
            try
            {
                var quizResults = await context.EmployeeResults
                    .Include(result => result.Persons)
                    .Include(result => result.Quizzes)
                    .Where(result => result.FK_PersonID == employeeId)
                    .ToListAsync();

                if (quizResults != null && quizResults.Any())
                {
                    var mappedResults = quizResults.Select(result => new EmployeeResultDTO
                    {
                        Username = result.Persons?.Username,
                        QuizHeading = result.Quizzes?.QuizHeading,
                        GuessedAnswer = result.GuessedAnswer,
                        isCorrect = result.isCorrect ? "Correct answer" : "Incorrect answer",
                        QuizDate = result.QuizDate.ToString(),
                        Points = result.Quizzes.Points
                        
                    }).ToList();

                    var apiResponse = new ApiResponse
                    {
                        Result = mappedResults,
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
                        Errors = new List<string>() { "No quiz results found for the employee" }
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateQuizResult([FromBody] CreateEmployeeResultDTO createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                EmployeeResult employeeResult = mapper.Map<EmployeeResult>(createDto);

                var checkAnswer = await context.Quizzes.FirstOrDefaultAsync(q => q.QuizID == createDto.FK_QuizID);
                if (checkAnswer != null)
                {
                    if (checkAnswer.CorrectAnswer.ToString() == createDto.GuessedAnswer.ToString())
                    {
                        employeeResult.isCorrect = true;
                    }
                    else
                    {
                        employeeResult.isCorrect = false;
                    }
                }
                else
                {
                    return BadRequest("Quiz not found");
                }

                employeeResult.QuizDate = DateTime.Now;

                await context.EmployeeResults.AddAsync(employeeResult);
                await context.SaveChangesAsync();

                apiResponse.Result = employeeResult;
                apiResponse.StatusCode = System.Net.HttpStatusCode.Created;
                apiResponse.IsSuccess = true;

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
    }
}
