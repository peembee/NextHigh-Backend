using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GoApptechBackend.Models
{
    public class EmployeeResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeResultID { get; set; }

        [Required]
        [ForeignKey("Persons")]
        public int FK_PersonID { get; set; }
        public virtual Person? Persons { get; set; }

        [Required]
        [ForeignKey("Quizzes")]
        public int FK_QuizID { get; set; }
        public virtual Quiz? Quizzes { get; set; }

        [Required]
        public DateTime QuizDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
