using DMS.Core.Entities.Identity;
using DMS.Core.Services;
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

namespace DMS.Services
{
    public class TokenService : ITokenService

    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager)
        {
            var AuthClaims = new List<Claim>()
{
            new Claim(ClaimTypes.GivenName,User.Name),
            new Claim(ClaimTypes.Email,User.Email)
};

            var UserRole = await userManager.GetRolesAsync(User);
            foreach (var Role in UserRole)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]));
            var Token = new JwtSecurityToken(
                        issuer: configuration["jwt:ValidIssure"],
                        audience: configuration["jwt:ValidAudiance"],
                        expires: DateTime.Now.AddDays(double.Parse(configuration["jwt:DurationInDays"])),
                        claims: AuthClaims,
                        signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                        );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
