using ApiAuthentification.Data;
using ApiAuthentification.Interface;
using ApiAuthentification.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAuthentification.Services
{
    public class LoginService : ILoginService
    {
        private readonly DataContext dataContext;

        public LoginService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public User Login(string name, string password)
        {
            var users = dataContext.Users.ToList();
            return users.FirstOrDefault(u => u.Name.Equals(name) && u.Password.Equals(password));

        }

        public string GenerateToken(string key, List<Claim> claims) 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public List<User> GetAll()
        {
            return dataContext.Users.ToList() ;
        }
    }
}
