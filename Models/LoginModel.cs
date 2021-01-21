using System.ComponentModel.DataAnnotations;

namespace client_server.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Password { get; set; }
    }
}
