using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models
{
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizID { get; set; }

        [Required]
        [StringLength(250)]
        public string QuizHeading { get; set; } = string.Empty;


        [Required]
        [StringLength(250)]
        public string AltOne { get; set; } = string.Empty;


        [Required]
        [StringLength(250)]
        public string AltTwo { get; set; } = string.Empty;


        [Required]
        [StringLength(250)]
        public string AltThree { get; set; } = string.Empty;


        [Required]
        [StringLength(25)]
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
