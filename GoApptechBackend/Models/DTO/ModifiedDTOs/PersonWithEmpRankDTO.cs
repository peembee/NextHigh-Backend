using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.ModifiedDTOs
{
    [NotMapped]
    public class PersonWithEmpRankDTO
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RankTitle { get; set; } = string.Empty;
    }
}
