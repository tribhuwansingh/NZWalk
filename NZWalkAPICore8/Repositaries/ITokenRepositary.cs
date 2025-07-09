using Microsoft.AspNetCore.Identity;

namespace NZWalkAPICore8.Repositaries
{
    public interface ITokenRepositary
    {
        public string GenerateJWTToken(IdentityUser user, List<string> roles);
    }
}
