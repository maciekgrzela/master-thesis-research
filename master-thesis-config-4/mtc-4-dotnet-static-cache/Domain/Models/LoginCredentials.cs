using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}