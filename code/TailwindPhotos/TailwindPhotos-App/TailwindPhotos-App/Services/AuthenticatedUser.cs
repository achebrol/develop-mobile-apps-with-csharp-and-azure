using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Tailwind.Photos.Services
{
    public class AuthenticatedUser
    {
        Dictionary<string, Provider> idpMatch = new Dictionary<string, Provider>()
        {
            ["facebook.com"] = Provider.Facebook,
            ["linkedin.com"] = Provider.LinkedIn
        };

        public AuthenticatedUser(string token)
        {
            var decoded = new JwtSecurityToken(token);
            Name = decoded.Claims.FirstOrDefault(c => c.Type == "name").Value;
            Email = decoded.Claims.FirstOrDefault(c => c.Type == "emails").Value;
            if (decoded.Claims.Any(c => c.Type == "idp"))
            {
                var p = decoded.Claims.First(c => c.Type == "idp").Value;
                Authenticator = (idpMatch.ContainsKey(p)) ? idpMatch[p] : Provider.Unknown;

                if (decoded.Claims.Any(c => c.Type == "idp_access_token"))
                {
                    AccessToken = decoded.Claims.FirstOrDefault(c => c.Type == "idp_access_token").Value;
                }
            }
            else
            {
                Authenticator = Provider.Username;
            }
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string AccessToken { get; private set; }
        public Provider Authenticator { get; private set; }

        public enum Provider
        {
            Unknown,
            Username,
            Facebook,
            LinkedIn
        };
    }
}
