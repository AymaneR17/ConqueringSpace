using ConqueringSpace.Model;
using System.Threading.Tasks;

namespace ConqueringSpace.Interfaces
{
    public interface ICelestialObjectService
    {
        public Task<string> CreateCelestialObject(string name, string image, int positionX, int positionY);
        public Task<string> UpdateCelestialObject(int id, string name, string image, int positionX, int positionY);
        public Task<string> DeleteCelestialObject(int id);
        public Task<List<CelestialObject>?> GetAll();
        public Task<CelestialObject> GetById(int id); 
       // public Task<string> UpdateUserCelestialObject(string userId, string celestialObjectName, int positionX, int positionY);
        public Task UpdateForUser(CelestialObject celestialObject); 


    }
}
