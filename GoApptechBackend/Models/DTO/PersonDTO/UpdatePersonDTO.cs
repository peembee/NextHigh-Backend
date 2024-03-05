using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    public class UpdatePersonDTO
    {
        public int PersonId { get; set; }


        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(40)]
        public string LastName { get; set; } = string.Empty;

        public double YearsInPratice { get; set; }

        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(250)]
        public string? ImageURL { get; set; } = string.Empty;
    }
}
