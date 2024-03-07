using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.ModifiedDTOs;
using GoApptechBackend.Models.DTO.PingPongResultDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/PongResult")]
    [ApiController]
    public class PingPongResultController : ControllerBase
    {
        private readonly ApplicationContext context;
        protected ApiResponse apiResponse;
        private readonly IMapper mapper;

        public PingPongResultController(ApplicationContext context, IMapper mapper)
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
                // Hämta alla personer från databasen tillsammans med deras respektive pingpong resultat
                var pingPongResults = await context.Persons.Include(p => p.PingPongResults).ToListAsync();

                var mappedResult = new List<PingPongResultDTO>();

                foreach (var person in pingPongResults)
                {
                    foreach (var pingPongResult in person.PingPongResults)
                    {
                        var resultDTO = new PingPongResultDTO
                        {
                            Username = person.Username,
                            MyPoints = pingPongResult.FK_PersonIDPoints,
                            OpponentUsername = pingPongResult.OpponentUsername,
                            OpponentPoints = pingPongResult.OpponentPoints,
                            WonMatch = pingPongResult.WonMatch ? "Victory" : "Defeat",
                            MatchDate = pingPongResult.MatchDate.ToString()
                        };

                        mappedResult.Add(resultDTO);
                    }
                }

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

        [HttpGet("{employeeId:int}", Name = "GetPingPongResults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetResultsById(int employeeId)
        {
            try
            {
                var person = await context.Persons.Include(p => p.PingPongResults).FirstOrDefaultAsync(p => p.PersonID == employeeId);

                if (person != null)
                {
                    var mappedResult = person.PingPongResults.Select(pingPongResult => new PingPongResultDTO
                    {
                        Username = person.Username,
                        MyPoints = pingPongResult.FK_PersonIDPoints,
                        OpponentUsername = pingPongResult.OpponentUsername,
                        OpponentPoints = pingPongResult.OpponentPoints,
                        WonMatch = pingPongResult.WonMatch ? "Victory" : "Defeat",
                        MatchDate = pingPongResult.MatchDate.ToString()
                    }).ToList();

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
