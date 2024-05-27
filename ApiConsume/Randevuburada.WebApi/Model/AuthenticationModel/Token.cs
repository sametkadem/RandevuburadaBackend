using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Randevuburada.WebApi.Model.AuthenticationModel
{
    public class Token
    {
        private readonly string _baseUrl;
        private readonly string _tokenKey;

        public Token(IConfiguration configuration)
        {
            _baseUrl = configuration.GetValue<string>("BaseUrl");
            _tokenKey = configuration.GetValue<string>("TokenKey");
        }

        public string Create()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Customer")
            };
            Console.WriteLine(claims[0].Value);
            var jwtToken = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(90),
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("this_is_my_dummy_secret_very_very_password")
                       ),
                   SecurityAlgorithms.HmacSha256Signature)
               );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string CreateComponyToken()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Company")
            };
            Console.WriteLine(claims[0].Value);
            var jwtToken = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(90),
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("this_is_my_dummy_secret_very_very_password")
                       ),
                   SecurityAlgorithms.HmacSha256Signature)
               );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string CreateAdminToken()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            Console.WriteLine(claims[0].Value);
            var jwtToken = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(90),
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("this_is_my_dummy_secret_very_very_password")
                       ),
                   SecurityAlgorithms.HmacSha256Signature)
               );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}