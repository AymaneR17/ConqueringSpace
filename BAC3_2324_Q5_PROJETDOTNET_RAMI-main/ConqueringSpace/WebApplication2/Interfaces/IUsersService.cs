using ConqueringSpace.Model;

namespace ConqueringSpace.Interfaces
{
    public interface IUsersService
    {
        public Task<string> Register(string name, string password, string role);
        public Task<List<Object>> Login(string name, string password);
        public Task<List<User>?> GetAll();
    }
}