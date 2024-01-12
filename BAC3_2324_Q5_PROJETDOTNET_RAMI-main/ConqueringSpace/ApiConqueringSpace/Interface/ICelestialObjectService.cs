using ApiConqueringSpace.Model;

namespace ApiConqueringSpace.Interface
{
    public interface ICelestialObjectService
    {
        public string Create(string name, string image, int positionX, int positionY);
        public string Update(int id, string name, string image, int positionX, int positionY);
        public string Delete(int id);
        public List<CelestialObject> GetAll();


    }
}
