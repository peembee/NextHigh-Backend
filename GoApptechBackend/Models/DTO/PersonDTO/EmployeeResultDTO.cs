using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    [NotMapped]
    public class EmployeeResultDTO
    {
        public string Username { get; set; } = string.Empty;
        public string QuizHeading { get; set; } = string.Empty;

        public string GuessedAnswer { get; set; } = string.Empty;

        public string QuizDate { get; set; } = string.Empty;

        public string isCorrect { get; set; } = string.Empty;

    }
}
