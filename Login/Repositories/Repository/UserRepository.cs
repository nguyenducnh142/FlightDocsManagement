using LoginService.DbContexts;
using LoginService.Models;
using JwtTokenAuthentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginService.Repositories.Interface;

namespace LoginService.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginContext _loginContext;
        public UserRepository(LoginContext loginContext)
        {
            _loginContext = loginContext;
        }

        public AuthenticationToken GenerateAuthToken(Login user)
        {
            var userLog = _loginContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.Status== true);

            if (userLog is null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(5);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, userLog.UserId),
            new Claim("RoleId", userLog.RoleId)
        };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7156",
                claims: claims,
                expires: expirationTimeStamp,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var authenticationToken = new AuthenticationToken()
            {
                Token = tokenString,
                ExpiresIn = (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            };

            return authenticationToken;
        }
    }
}
