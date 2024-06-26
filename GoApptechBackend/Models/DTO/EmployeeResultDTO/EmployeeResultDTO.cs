﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.EmployeeResultDTO
{
    [NotMapped]
    public class EmployeeResultDTO
    {
        public int EmployeeResultID { get; set; }
        public int FK_QuizID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string QuizHeading { get; set; } = string.Empty;

        public string GuessedAnswer { get; set; } = string.Empty;

        public string QuizDate { get; set; } = string.Empty;

        public int Points { get; set; }

        public string isCorrect { get; set; } = string.Empty;

    }
}
