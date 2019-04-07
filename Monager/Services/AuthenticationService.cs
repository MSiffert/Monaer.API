using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Monager.Database;
using Monager.Models;

namespace Monager.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private AppSettings AppSettings { get; }
        public MonagerDbContext DbContext { get; }
        private readonly TimeSpan _tokenExpiration = TimeSpan.FromDays(7);

        public AuthenticationService(IOptions<AppSettings> appSettings, MonagerDbContext dbContext)
        {
            DbContext = dbContext;
            this.AppSettings = appSettings.Value;
        }

        public async Task<Token> AuthenticateAsync(string loginKey)
        {
            var user = await this.DbContext.Users.FirstOrDefaultAsync(e => e.LoginKey == loginKey);

            if (user == null)
                throw new UnauthorizedAccessException();

            var token = GenerateToken(user.Name);
            await this.DbContext.SaveChangesAsync();

            return token;
        }

        public Token GenerateToken(string claimValue)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, claimValue) }),
                Expires = DateTime.UtcNow + _tokenExpiration,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.AppSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            return new Token
            {
                BearerToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
            };
        }
    }

    public interface IAuthenticationService
    {
        Task<Token> AuthenticateAsync(string loginKey);
    }
}