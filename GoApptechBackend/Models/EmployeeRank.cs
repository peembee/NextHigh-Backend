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
        public string EmployeeRankName { get; set; } = string.Empty;
        public virtual ICollection<EmployeePoints>? EmployeePoints { get; set; }
    }
}
