using BackEndAlbergue.Data.Repository;
using BackEndAlbergue.Models.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IRefuge _refugeRepository;
        public UserService(IRefuge refugeRepository,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._refugeRepository = refugeRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        public async Task<UserManagerResponse> CreateRoleAsync(CreateRoleViewModel model)
        {
            var identityRole = new IdentityRole()
            {
                Name = model.Name,
                NormalizedName = model.NormalizedName
            };

            var result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Role created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Role did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> CreateUserRoleAsync(CreateUserRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return new UserManagerResponse
                {
                    Message = "Role does not exist",
                    IsSuccess = false
                };
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "user does not exist",
                    IsSuccess = false
                };
            }

            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                return new UserManagerResponse
                {
                    Message = "user has role already",
                    IsSuccess = false
                };
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Role assigned",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "something went wrong",
                IsSuccess = false
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }


            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(model));
            }

            return RegisterUserInternalAsync(model);
        }

        public async Task<UserManagerResponse> RegisterUserInternalAsync(RegisterViewModel model)
        {

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public IEnumerable<UserModel> GetUsersAsync() 
        {
            return  _refugeRepository.GetUsers();
        }
        public Task<bool> UpdateUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(model));
            }

            return UpdateUserInternalAsync(model);

        }

        public Task<bool> UpdateUserInternalAsync(RegisterViewModel model)
        {

            if (model.Password != model.ConfirmPassword)
                throw new ArgumentException(nameof(model));

            return UpdateUserInternalPasswordAsync(model);

        }

        public async Task<bool> UpdateUserInternalPasswordAsync(RegisterViewModel model)
        {
            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var user = await userManager.FindByEmailAsync(identityUser.Email);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new ArgumentException(nameof(model));
            }
        }
    }
}
