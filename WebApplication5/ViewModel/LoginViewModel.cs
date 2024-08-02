using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [MaxLength(16)]
        [Required]
        public  string Password { get; set; }

    }
}
