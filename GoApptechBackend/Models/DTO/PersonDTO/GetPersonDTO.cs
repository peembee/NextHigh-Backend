using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    public class GetPersonDTO
    {
        public int PersonID { get; set; }

        public bool isAdmin { get; set; }

        public int FK_PingPongRankID { get; set; }

        public int FK_EmployeeRankID { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int EmpPoints { get; set; }

        public double YearsInPratice { get; set; }

        public string? ImageURL { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

    }
}
