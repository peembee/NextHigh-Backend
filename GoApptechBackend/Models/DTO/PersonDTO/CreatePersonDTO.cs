using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    public class CreatePersonDTO
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public double YearsInPratice { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}
