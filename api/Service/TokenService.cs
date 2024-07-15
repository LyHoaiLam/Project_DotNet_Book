using System.Security.Claims;
using System.Text;
using api.interfaces;
using api.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace api.Service {
    public class TokenService : ITokenService {

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;


//Lấy content setting từ Appliacation Json
        public TokenService(IConfiguration config) {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        
        public string CreateToken(AppUser user) {
            // Để nhận dạng user và thể hei65n nhg gì user không thể làm trong Project của chúng ta
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptior = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(token);
            // throw new NotImplementedException();
        }
    }
}