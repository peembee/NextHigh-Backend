using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models
{
    public class EmployeeRank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeRankID { get; set; }

        [Required]
        [StringLength(50)]
        public string RankTitle { get; set; } = string.Empty;

        [Required]
        public int RequiredPoints { get; set; }

        public virtual ICollection<Person>? Persons { get; set; }
    }
}
