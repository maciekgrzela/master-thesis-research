using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class SignedUser : User
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}