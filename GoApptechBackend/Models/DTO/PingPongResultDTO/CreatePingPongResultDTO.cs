using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models.DTO.PingPongResultDTO
{
    [NotMapped]
    public class CreatePingPongResultDTO
    {
        public int FK_PersonID { get; set; }

        public int FK_PersonIDPoints { get; set; }

        public int OpponentPoints { get; set; }

        public string OpponentUsername { get; set; } = string.Empty;

    }
}
