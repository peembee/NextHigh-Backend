using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Models;
using AutoMapper;

namespace GoApptechBackend.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Person, GetPersonDTO>().ReverseMap();

            CreateMap<Person, CreatePersonDTO>().ReverseMap();
            CreateMap<Person, UpdatePersonDTO>().ReverseMap();
        }
    }
}
