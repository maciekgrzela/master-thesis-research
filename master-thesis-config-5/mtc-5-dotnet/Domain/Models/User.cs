using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(300)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public override string UserName { get; set; }
    }
}