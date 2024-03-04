using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models
{
    public class PingPongRank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PingPongRankID { get; set; }


        [Required]
        [StringLength(50)]
        public string RankTitle { get; set; } = string.Empty;

        [Required]
        public int RequiredWinnings { get; set; }

    }
}
