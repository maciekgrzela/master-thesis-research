using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Auth
{
    public class LoginCredentialsResource
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}