using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var bytes = Encoding.UTF8.GetBytes(_tokenKey);

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(issuer: _baseUrl, audience: _baseUrl,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(3), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public string CreateComponyToken()
        {
            var bytes = Encoding.UTF8.GetBytes(_tokenKey);

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Compony"),
                new Claim(ClaimTypes.Role,"Visitor")
            };
            JwtSecurityToken token = new JwtSecurityToken(issuer: _baseUrl, audience: _baseUrl,
                               notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(3), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public string CreateAdminToken()
        {
            var bytes = Encoding.UTF8.GetBytes(_tokenKey);

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
            JwtSecurityToken token = new JwtSecurityToken(issuer: _baseUrl, audience: _baseUrl,
                               notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(3), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}