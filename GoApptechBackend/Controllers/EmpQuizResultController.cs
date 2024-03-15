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
                         EmployeeResultID = result.EmployeeResultID,
                        Username = result.Persons?.Username,
                        QuizHeading = result.Quizzes?.QuizHeading,
                        GuessedAnswer = result.GuessedAnswer,
                        isCorrect = result.isCorrect ? "Correct answer" : "Incorrect answer",
                        QuizDate = result.QuizDate.ToString(),
                        Points = result.Quizzes.Points,
                        FK_QuizID = result.Quizzes.QuizID,
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
                        EmployeeResultID = result.EmployeeResultID,
                        Username = result.Persons?.Username,
                        QuizHeading = result.Quizzes?.QuizHeading,
                        GuessedAnswer = result.GuessedAnswer,
                        isCorrect = result.isCorrect ? "Correct answer" : "Incorrect answer",
                        QuizDate = result.QuizDate.ToString(),
                        Points = result.Quizzes.Points,
                        FK_QuizID = result.Quizzes.QuizID,
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
                        Result = new List<EmployeeResultDTO>(),
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
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
                var updateEmployeePoints = await context.Persons.FirstOrDefaultAsync(p => p.PersonID == createDto.FK_PersonID);
                var checkAnswer = await context.Quizzes.FirstOrDefaultAsync(q => q.QuizID == createDto.FK_QuizID);
                var updateRanks = await context.EmployeeRanks.ToListAsync();

                if (checkAnswer != null)
                {
                    if (checkAnswer.CorrectAnswer.ToString() == createDto.GuessedAnswer.ToString())
                    {
                        employeeResult.isCorrect = true;

                        foreach (var points in updateRanks)
                        {
                            if (updateEmployeePoints.EmpPoints + checkAnswer.Points >= points.RequiredPoints)
                            {
                                updateEmployeePoints.FK_EmployeeRankID = points.EmployeeRankID;
                            }
                        }
                        updateEmployeePoints.EmpPoints += checkAnswer.Points;
                    }
                    else
                    {
                        employeeResult.isCorrect = false;
                        updateEmployeePoints.EmpPoints -= checkAnswer.Points;

                        if(updateEmployeePoints.EmpPoints < 1)
                        {
                            updateEmployeePoints.EmpPoints = 0;
                        }
                    }
                    
                    context.Persons.Update(updateEmployeePoints);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Quiz not found");
                }

                employeeResult.QuizDate = DateTime.Now;

                await context.EmployeeResults.AddAsync(employeeResult);
                await context.SaveChangesAsync();

                apiResponse.Result = new
                {
                    GuessedAnswer = employeeResult.GuessedAnswer,
                    IsCorrect = employeeResult.isCorrect,
                    CorrectAnswer = checkAnswer.CorrectAnswer
                };
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
