using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models.DTO.QuizDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoApptechBackend.Controllers
{
    [Route("api/UnAsweredQuizzes")]
    [ApiController]
    public class UnAnsweredQuizziesController : ControllerBase
    {
        private readonly ApplicationContext context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public UnAnsweredQuizziesController(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.apiResponse = new ApiResponse();
            this.mapper = mapper;
        }

        [HttpGet("{employeeId:int}", Name = "GetUnAsweredQuizzezById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GettUnAsweredQuizzezById(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                {
                    return BadRequest("Invalid Quiz-ID");
                }

                var quizzes = await context.Quizzes
                    .Where(q => !q.EmployeeResults.Any(er => er.FK_PersonID == employeeId && er.isCorrect))
                    .ToListAsync();

                if (quizzes == null || quizzes.Count == 0)
                {
                    string quizData = "MissingData";
                    return Ok(new ApiResponse
                    {
                        Result = quizData,
                        IsSuccess = true
                    });
                }


                // Välj en slumpmässig fråga från listan
                var random = new Random();
                var randomIndex = random.Next(0, quizzes.Count);
                var randomQuiz = quizzes[randomIndex];

                var quizDto = mapper.Map<QuizDTO>(randomQuiz);

                var apiResponse = new ApiResponse
                {
                    Result = quizDto,
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
    }
}
