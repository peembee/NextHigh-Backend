using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GoApptechBackend.Models.DTO.PingPongResultDTO
{
    [NotMapped]
    public class PingPongResultDTO
    {
        public int PingPongResultID { get; set; }
        public string MatchGuid { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int MyPoints { get; set; }
        public string OpponentUsername { get; set; } = string.Empty;
        public int OpponentPoints { get; set; }
        public string WonMatch { get; set; } = string.Empty;
        public string MatchDate { get; set; } = string.Empty;
    }
}
