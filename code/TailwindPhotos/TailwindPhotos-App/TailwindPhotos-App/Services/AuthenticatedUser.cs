using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Tailwind.Photos.Services
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(string token)
        {
            var decoded = new JwtSecurityToken(token);
            Name = decoded.Claims.FirstOrDefault(c => c.Type == "name").Value;
            Email = decoded.Claims.FirstOrDefault(c => c.Type == "emails").Value;
            AccessToken = decoded.Claims.FirstOrDefault(c => c.Type == "idp_access_token").Value;

            var p = decoded.Claims.First(c => c.Type == "idp").Value;
            if (p == "facebook.com") {
                Authenticator = Provider.Facebook;
            } else if (p == "linkedin.com") {
                Authenticator = Provider.LinkedIn;
            } else {
                Authenticator = Provider.Username;
            }
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string AccessToken { get; private set; }
        public Provider Authenticator { get; private set; }

        public enum Provider
        {
            Username,
            Facebook,
            LinkedIn
        };
    }
}
