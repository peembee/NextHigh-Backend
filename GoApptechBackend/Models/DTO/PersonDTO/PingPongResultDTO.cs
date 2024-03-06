using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GoApptechBackend.Models.DTO.PersonDTO
{
    [NotMapped]
    public class PingPongResultDTO
    {
        public string Username { get; set; } = string.Empty;
        public string MyPoints { get; set; } = string.Empty;


        public string OpponentUsername { get; set; } = string.Empty;
        public int OpponentPoints { get; set; }

        public string WonMatch { get; set; } = string.Empty;

        public string MatchDate { get; set; } = string.Empty;
    }
}
