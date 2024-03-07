using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.EmployeeResultDTO
{
    [NotMapped]
    public class CreateEmployeeResultDTO
    {
        [Required]
        public int FK_PersonID { get; set; }

        [Required]
        public int FK_QuizID { get; set; }

        [Required]
        [StringLength(250)]
        public string GuessedAnswer { get; set; } = string.Empty;
    }
}
