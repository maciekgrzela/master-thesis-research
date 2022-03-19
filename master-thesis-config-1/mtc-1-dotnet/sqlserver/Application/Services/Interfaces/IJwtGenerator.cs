using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateToken(User user, string role);
    }
}