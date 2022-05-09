using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<SignedUser>> Login(LoginCredentials credentials);
        Task<Response<SignedUser>> CurrentLoggedUser();
        Task<Response<SignedUser>> Register(RegisterCredentials credentials);
    }
}