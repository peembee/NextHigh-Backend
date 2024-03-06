using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    [NotMapped]
    public class QuizDTO
    {
        public string QuizHeading { get; set; } = string.Empty;

        public string AltOne { get; set; } = string.Empty;

        public string AltTwo { get; set; } = string.Empty;

        public string AltThree { get; set; } = string.Empty;

        public int Points { get; set; }

    }
}
