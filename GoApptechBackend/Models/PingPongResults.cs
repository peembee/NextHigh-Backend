using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GoApptechBackend.Models
{
    public class PingPongResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PingPongResultID { get; set; }

        [Required]
        [ForeignKey("Persons")]
        public int FK_PersonID { get; set; }
        public virtual Person? Persons { get; set; }

        [Required]
        public int FK_PersonIDPoints { get; set; }

        [Required]
        public int OpponentPoints { get; set; }

        [Required] 
        public string OpponentUsername { get; set; } = string.Empty;

        [Required]
        public bool WonMatch { get; set; }

        [Required]
        public string MatchGuid { get; set; } = string.Empty;

        [Required]
        public DateTime MatchDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
