using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.ModifiedDTOs;
using GoApptechBackend.Models.DTO.PingPongResultDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

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
                            MatchGuid = pingPongResult.MatchGuid,
                            PingPongResultID = pingPongResult.PingPongResultID,
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
                        MatchGuid = pingPongResult.MatchGuid,
                        PingPongResultID = pingPongResult.PingPongResultID,
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreatePongResult([FromBody] CreatePingPongResultDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            try
            {
                Guid uniqueId = Guid.NewGuid();
                string guidString = uniqueId.ToString();

                PingPongResults pingPongResult = mapper.Map<PingPongResults>(createDto);

                pingPongResult.WonMatch = createDto.FK_PersonIDPoints > createDto.OpponentPoints ? true : false;
                pingPongResult.MatchDate = DateTime.Now;
                pingPongResult.MatchGuid = guidString;

                await context.PingPongResults.AddAsync(pingPongResult);

                var userNameFromDTO = await context.Persons.FirstOrDefaultAsync(p => p.PersonID == createDto.FK_PersonID);
                var opponentID = await context.Persons.FirstOrDefaultAsync(p => p.Username == pingPongResult.OpponentUsername);

                // updating Ranks
                if (pingPongResult.WonMatch == true)
                {
                    
                    var pingPongRanks = await context.PingPongRanks.ToListAsync();
                     
                    
                    foreach( var rank in pingPongRanks)
                    {
                        if (userNameFromDTO.PongVictories + 1 >= rank.RequiredWinnings)
                        {
                            userNameFromDTO.FK_PingPongRankID = rank.PingPongRankID;
                        }
                    }
                    userNameFromDTO.PongVictories = userNameFromDTO.PongVictories += 1;
                    context.Persons.Update(userNameFromDTO);
                    await context.SaveChangesAsync();
                }
               


                // update opponent result
                PingPongResults opponent = new PingPongResults();
                opponent.FK_PersonID = opponentID.PersonID;
                opponent.MatchGuid = guidString;
                opponent.OpponentPoints = pingPongResult.FK_PersonIDPoints;
                opponent.FK_PersonIDPoints = pingPongResult.OpponentPoints;
                opponent.OpponentUsername = userNameFromDTO.Username;
                opponent.WonMatch = opponent.FK_PersonIDPoints > opponent.OpponentPoints ? true : false;
                opponent.MatchDate = pingPongResult.MatchDate;

                // update opponents rank
                if (pingPongResult.WonMatch == false)
                {
                    var pingPongRanks = await context.PingPongRanks.ToListAsync();
                   
                    foreach (var rank in pingPongRanks)
                    {
                        if (opponentID.PongVictories +1 >= rank.RequiredWinnings)
                        {
                            opponentID.FK_PingPongRankID = rank.PingPongRankID;
                        }
                    }
                    opponentID.PongVictories = opponentID.PongVictories += 1;
                    context.Persons.Update(opponentID);
                    await context.SaveChangesAsync();
                }
                
                await context.PingPongResults.AddAsync(opponent);
                await context.SaveChangesAsync();

                apiResponse.Result = pingPongResult;
                apiResponse.StatusCode = System.Net.HttpStatusCode.Created;
                apiResponse.IsSuccess = true;

                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var json = JsonSerializer.Serialize(apiResponse, jsonOptions);

                return Content(json, "application/json");
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
