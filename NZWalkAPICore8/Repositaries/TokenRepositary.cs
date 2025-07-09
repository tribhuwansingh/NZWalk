using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWalkAPICore8.Repositaries
{
    public class TokenRepositary : ITokenRepositary
    {
        private readonly IConfiguration configuration;
        public TokenRepositary(IConfiguration _configuration) 
        {
            this.configuration = _configuration;
        }
        public string GenerateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create Claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentital = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(configuration["Jwt:Issurer"], configuration["Jwt:Audience"], claims,expires:DateTime.Now.AddMinutes(15),signingCredentials : credentital);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
