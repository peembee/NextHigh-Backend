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

        public int YearsInPratice { get; set; }

        public int LossesInPingPong { get; set; }

        public int WinningsInPingPong { get; set; }
    }
}
