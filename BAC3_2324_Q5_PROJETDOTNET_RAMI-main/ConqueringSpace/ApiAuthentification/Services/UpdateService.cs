using ApiAuthentification.Data;
using ApiAuthentification.Interface;

namespace ApiAuthentification.Services
{
    public class UpdateService : IUpdateService
    {
        DataContext dataContext;

        public UpdateService(DataContext dataContext) {
        
                this.dataContext = dataContext;
        }   


        public string Update(int id, string name,  string role)
        {

            
                var user = dataContext.Users.FirstOrDefault(u => u.Id.Equals(id));

                user.Name = name;
              
                user.Role = role;

                try
                {
                    dataContext.Users.UpdateRange(user);
                    dataContext.SaveChanges();

                    return "OK";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("an error occured to update a user " + ex.Message);
                }
                return "Not OK";

            
        }
    }
}
