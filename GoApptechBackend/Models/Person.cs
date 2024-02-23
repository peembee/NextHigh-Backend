using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GoApptechBackend.Models
{
    public class Person
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonID { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;


        [Required]
        [StringLength(40)]
        public string LastName { get; set; } = string.Empty;


        [Required]
        public double YearsInPratice { get; set; }


        [Required]
        public int EmpPoints { get; set; } = 0;



        [Required]
        public int PongPoints { get; set; } = 0;

        public int LossesInPingPong { get; set; } = 0;

        public int WinningsInPingPong { get; set; } = 0;

        [StringLength(250)]
        public string? ImageURL { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

    }
}
