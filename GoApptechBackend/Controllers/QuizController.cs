﻿using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.QuizDTO;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

                var mappResult = mapper.Map<List<QuizDTO>>(quizzes);

                var apiResponse = new ApiResponse
                {
                    Result = mappResult,
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

        [HttpGet("{id:int}", Name = "GetQuizById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetResultsById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Quiz-ID");
                }

                var quiz = await context.GetAsync(q => q.QuizID == id);

                if (quiz == null)
                {
                    return NotFound("Quiz not found");
                }

                var quizDto = mapper.Map<QuizDTO>(quiz);

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
