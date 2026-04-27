using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Musicana.Api.Models;
using Musicana.Api.Requests;
using Musicana.Api.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Musicana.Api.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new AuthResponse { Message = "Email already exists!" };

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new AuthResponse { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

            await _userManager.AddToRoleAsync(user, "User");

            var token = await CreateTokenAsync(user);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Email = user.Email,
                Username = user.UserName,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new AuthResponse { Message = "Email or Password is incorrect!" };

            var token = await CreateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Email = user.Email!,
                Username = user.UserName!,
                Roles = roles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo
            };
        }

        private async Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"]!)),
                signingCredentials: creds
            );
        }

       
    }
    }

