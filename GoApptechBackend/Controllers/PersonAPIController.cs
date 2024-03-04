using AutoMapper;
using GoApptechBackend.APIResponse;
using GoApptechBackend.Models;
using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Repository.Irepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoApptechBackend.Controllers
{
    [Route("api/Person")]
    [ApiController]
    public class PersonAPIController : ControllerBase
    {
        private readonly IRepository<Person> context;
        private readonly IMapper mapper;
        protected ApiResponse apiResponse;

        public PersonAPIController(IRepository<Person> context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.apiResponse = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPerson()
        {
            try
            {
                IEnumerable<Person> personList = await context.GetAllAsync();
                apiResponse.Result = mapper.Map<List<GetPersonDTO>>(personList);
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Errors = new List<string>() { ex.ToString() };
            }

            return apiResponse;
        }


        [HttpGet("{id:int}", Name = "GetPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetPeople(int id)
        {
            try
            {
                if (id == 0)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(apiResponse);
                }
                var person = await context.GetAsync(p => p.PersonID == id);
                if (person == null)
                {
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(apiResponse);
                }
                apiResponse.Result = mapper.Map<GetPersonDTO>(person);
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Errors
                    = new List<string>() { ex.ToString() };
            }
            return apiResponse;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreatePerson([FromBody] CreatePersonDTO createDto)
        {
            try
            {
                if (await context.GetAsync(ap => ap.Username.ToLower() == createDto.Username.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom error", "This user alredy exist");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                createDto.FirstName = char.ToUpper(createDto.FirstName[0]) + createDto.FirstName.Substring(1).ToLower();
                createDto.LastName = char.ToUpper(createDto.LastName[0]) + createDto.LastName.Substring(1).ToLower();


                Person person = mapper.Map<Person>(createDto);
                await context.CreateAsync(person);
                apiResponse.Result = mapper.Map<CreatePersonDTO>(person);
                apiResponse.StatusCode = System.Net.HttpStatusCode.Created;
                apiResponse.IsSuccess = true;
                return CreatedAtRoute("GetPeople", new { id = person.PersonID }, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Errors = new List<string>() { ex.ToString() };
            }
            return apiResponse;
        }


        [HttpPut("{id:int}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdatePerson(int id, [FromBody] UpdatePersonDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.PersonId)
                {
                    return BadRequest();
                }

                // Hämta befintlig Person från databasen
                Person existingPerson = await context.GetAsync(p => p.PersonID == id);

                if (existingPerson == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrWhiteSpace(updateDto.FirstName) && existingPerson.FirstName != updateDto.FirstName)
                {
                    existingPerson.FirstName = char.ToUpper(updateDto.FirstName[0]) + updateDto.FirstName.Substring(1).ToLower();
                }

                if (!string.IsNullOrWhiteSpace(updateDto.LastName) && existingPerson.LastName != updateDto.LastName)
                {
                    existingPerson.LastName = char.ToUpper(updateDto.LastName[0]) + updateDto.LastName.Substring(1).ToLower();
                }


                if (updateDto.Email != existingPerson.Email && existingPerson.Email != updateDto.Email)
                {
                    existingPerson.Email = updateDto.Email;
                }

                if (!string.IsNullOrWhiteSpace(updateDto.ImageURL) && existingPerson.ImageURL != updateDto.ImageURL)
                {
                    existingPerson.ImageURL = char.ToUpper(updateDto.ImageURL[0]) + updateDto.ImageURL.Substring(1).ToLower();
                }

                if (updateDto.YearsInPratice != existingPerson.YearsInPratice && updateDto.YearsInPratice > 0)
                {
                    existingPerson.YearsInPratice = updateDto.YearsInPratice;
                }

                if (updateDto.EmpPoints != existingPerson.EmpPoints && updateDto.EmpPoints > 0)
                {
                    existingPerson.EmpPoints = updateDto.EmpPoints;
                }


                await context.UpdateAsync(existingPerson);

                apiResponse.Result = mapper.Map<UpdatePersonDTO>(updateDto);
                apiResponse.StatusCode = HttpStatusCode.NoContent;
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Errors = new List<string>() { ex.ToString() };
            }
            return apiResponse;
        }


        [HttpDelete("{id:int}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeletePerson(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var person = await context.GetAsync(p => p.PersonID == id);
                if (person == null)
                {
                    return NotFound();
                }
                await context.RemoveAsync(person);
                apiResponse.StatusCode = HttpStatusCode.NoContent;
                apiResponse.IsSuccess = true;
                apiResponse.Result = new { Message = "Successfully Deleted" };
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Errors = new List<string>() { ex.ToString() };
            }
            return apiResponse;
        }
    }
}
