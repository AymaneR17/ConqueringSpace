using ApiAuthentification.Data;
using ApiAuthentification.Interface;

namespace ApiAuthentification.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly DataContext dataContext;

        public DeleteService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public string Delete(int id)
        {

            var users = dataContext.Users.ToList();
            var user =  users.FirstOrDefault(u => u.Id.Equals(id));

            try
            {
                dataContext.Remove(user);
                dataContext.SaveChanges();
   
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error to delete user" + ex.Message);

               
            }
            if (user != null)
            {
                return "user : " + user.Name + " has been deleted";
            }
            return "not found";
        }
    }
}
