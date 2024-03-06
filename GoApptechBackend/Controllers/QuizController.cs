using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/Quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IRepository<Quiz> context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public QuizController(IRepository<Quiz> context, IMapper mapper)
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
                IEnumerable<Quiz> quizzes = await context.GetAllAsync();

                var mappedResult = quizzes.Select(quiz => new QuizDTO
                {
                    QuizHeading = quiz.QuizHeading,
                    AltOne = quiz.AltOne,
                    AltTwo = quiz.AltTwo,
                    AltThree = quiz.AltThree,
                    Points = quiz.Points
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

        //[HttpGet("{id:int}", Name = "GetQuizById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<ApiResponse>> GetResultsById(int id)
        //{
        //    try
        //    {
        //        var personWithRank = await context
        //         .Include(table => table.PingPongRanks)
        //         .Where(person => person.PersonID == id)
        //         .FirstOrDefaultAsync();

        //        if (personWithRank != null)
        //        {
        //            var mappedResult = new PersonWithRankDTO
        //            {
        //                Username = personWithRank.Username,
        //                FirstName = personWithRank.FirstName,
        //                LastName = personWithRank.LastName,
        //                RankTitle = personWithRank.PingPongRanks?.RankTitle ?? "Unknown"
        //            };

        //            var apiResponse = new ApiResponse
        //            {
        //                Result = mappedResult,
        //                StatusCode = HttpStatusCode.OK,
        //                IsSuccess = true
        //            };

        //            return Ok(apiResponse);
        //        }
        //        else
        //        {
        //            var apiResponse = new ApiResponse
        //            {
        //                IsSuccess = false,
        //                Errors = new List<string>() { "Person not found" }
        //            };

        //            return NotFound(apiResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var apiResponse = new ApiResponse
        //        {
        //            IsSuccess = false,
        //            Errors = new List<string>() { ex.ToString() }
        //        };
        //        return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
        //    }
        //}
    }
}
