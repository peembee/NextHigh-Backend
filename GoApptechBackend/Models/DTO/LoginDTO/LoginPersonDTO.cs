using System.ComponentModel.DataAnnotations;

namespace GoApptechBackend.Models.DTO.LoginDTO
{
    public class LoginPersonDTO
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
