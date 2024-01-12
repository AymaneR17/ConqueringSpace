using ApiAuthentification.Data;
using ApiAuthentification.Interface;
using ApiAuthentification.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiAuthentification.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly DataContext dataContext;

        public RegisterService(DataContext dataContext) {
            this.dataContext = dataContext;
        }
        public string Register(string name, string password, string role)
        {
            User user = new User() { 
               Name = name,
               Password = password,
               Role = role
            };

            try
            {
                dataContext.Users.AddRange(user);
                dataContext.SaveChanges();

                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("an error occured to register a user " + ex.Message);
            }
            return "Not OK";
            
        }
    }
}
