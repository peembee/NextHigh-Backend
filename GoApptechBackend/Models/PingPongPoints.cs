using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoApptechBackend.Models
{
    public class PingPongPoints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PingPongPointsID { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        [ForeignKey("PingPongRanks")]
        public int FK_PongRankID { get; set; }
        public virtual PingPongRank? PingPongRanks { get; set; }

        public virtual ICollection<Person> Persons { get; set; }

    }
}
