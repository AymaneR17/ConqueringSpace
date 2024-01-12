using ApiAuthentification.Model;
using System.Security.Claims;

namespace ApiAuthentification.Interface
{
    public interface ILoginService
    {
        User Login(string name, string password);
        string GenerateToken(string key, List<Claim> claims);
        public List<User> GetAll();


    }
}
