﻿using GoApptechBackend.Models.DTO.PersonDTO;
using GoApptechBackend.Models;
using AutoMapper;
using GoApptechBackend.Models.DTO.LoginDTO;
using GoApptechBackend.Models.DTO.QuizDTO;
using GoApptechBackend.Models.DTO.EmployeeResultDTO;
using GoApptechBackend.Models.DTO.PingPongResultDTO;

namespace GoApptechBackend.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Person, GetPersonDTO>().ReverseMap();

            CreateMap<Person, CreatePersonDTO>().ReverseMap();

            CreateMap<Person, UpdatePersonDTO>().ReverseMap();

            CreateMap<Person, LoginPersonDTO>().ReverseMap();

            CreateMap<Quiz, QuizDTO>().ReverseMap();

            CreateMap<CreateEmployeeResultDTO, EmployeeResult>().ReverseMap();

            CreateMap<CreatePingPongResultDTO, PingPongResults>().ReverseMap();

            CreateMap<Quiz, QuizDTO>().ReverseMap();

            CreateMap<Quiz, CreateQuizDTO>().ReverseMap();
        }
    }
}
