using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Auth;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;
        public AuthController(IMapper mapper, IAuthService authService) : base(mapper)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCredentialsResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.Unauthorized, ModelState.GetErrorMessages());
            }

            var authCredentials = mapper.Map<LoginCredentialsResource, LoginCredentials>(resource);
            var result = await authService.Login(authCredentials);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var signedUser = mapper.Map<SignedUser, SignedUserResource>(result.Type);

            return GenerateResponse<SignedUserResource>(result.Status, signedUser);
        }

        [HttpGet("current")]
        public async Task<IActionResult> CurrentLoggedUserAsync()
        {
            var result = await authService.CurrentLoggedUser();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var signedUser = mapper.Map<SignedUser, SignedUserResource>(result.Type);

            return GenerateResponse<SignedUserResource>(result.Status, signedUser);
        }

        [AllowAnonymous]
        #warning ChangeToAuthorizedWithRoleAdmin
        // [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCredentialsResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.Unauthorized, ModelState.GetErrorMessages());
            }

            var authCredentials = mapper.Map<RegisterCredentialsResource, RegisterCredentials>(resource);
            var result = await authService.Register(authCredentials);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var signedUser = mapper.Map<SignedUser, SignedUserResource>(result.Type);

            return GenerateResponse<SignedUserResource>(result.Status, signedUser);
        }
    }
}