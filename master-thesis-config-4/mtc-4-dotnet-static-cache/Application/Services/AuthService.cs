using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IJwtGenerator jwtGenerator;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserAccessor userAccessor;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
            this.jwtGenerator = jwtGenerator;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Response<SignedUser>> CurrentLoggedUser()
        {
            var user = await userManager.Users
                .SingleOrDefaultAsync(w => w.UserName == userAccessor.GetLoggedUserName());


            if (user == null)
            {
                return new Response<SignedUser>(HttpStatusCode.Unauthorized, "There is no currently logged user");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var signedUser = new SignedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = userRoles[0],
                Token = jwtGenerator.CreateToken(user, userRoles[0])
            };

            return new Response<SignedUser>(HttpStatusCode.OK, signedUser);
        }

        public async Task<Response<SignedUser>> Login(LoginCredentials credentials)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == credentials.UserName);

            if (user == null)
            {
                return new Response<SignedUser>(HttpStatusCode.Unauthorized, $"User with username:{credentials.UserName} does not exist");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            var userRoles = await userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                var signedUser = new SignedUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Role = userRoles[0],
                    Token = jwtGenerator.CreateToken(user, userRoles[0])
                };

                return new Response<SignedUser>(HttpStatusCode.OK, signedUser);
            }

            return new Response<SignedUser>(HttpStatusCode.Unauthorized, "Invalid credentials");
        }

        public async Task<Response<SignedUser>> Register(RegisterCredentials credentials)
        {
            var existingUser = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == credentials.UserName);

            if (existingUser != null)
            {
                return new Response<SignedUser>(HttpStatusCode.Unauthorized, $"User with username: {credentials.UserName} already exists");
            }

            var existingRole = await roleManager.RoleExistsAsync(credentials.Role);

            if (!existingRole)
            {
                return new Response<SignedUser>(HttpStatusCode.Unauthorized, $"Role with name: {credentials.Role} does not exist");
            }

            var user = new User
            {
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                UserName = credentials.UserName
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return new Response<SignedUser>(HttpStatusCode.Unauthorized, $"Password doesn't match validation criteria");
            }

            await userManager.AddToRoleAsync(user, credentials.Role);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, credentials.Role));

            var signedUser = new SignedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = credentials.Role,
                Token = jwtGenerator.CreateToken(user, credentials.Role)
            };

            return new Response<SignedUser>(HttpStatusCode.OK, signedUser);
        }
    }
}