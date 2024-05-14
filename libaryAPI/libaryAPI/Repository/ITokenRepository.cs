using Microsoft.AspNetCore.Identity;

namespace libaryAPI.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
