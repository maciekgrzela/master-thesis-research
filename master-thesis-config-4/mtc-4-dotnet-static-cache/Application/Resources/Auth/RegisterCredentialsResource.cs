using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Auth
{
    public class RegisterCredentialsResource
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
    }
}