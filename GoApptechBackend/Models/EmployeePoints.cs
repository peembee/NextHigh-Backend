using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models
{
    public class EmployeePoints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeePointsID { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        [ForeignKey("EmployeeRanks")]
        public int FK_EmployeeRankID { get; set; }
        public virtual EmployeeRank? EmployeeRanks { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
