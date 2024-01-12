using ApiConqueringSpace.Data;
using ApiConqueringSpace.Interface;
using ApiConqueringSpace.Model;

namespace ApiConqueringSpace.Services
{
    public class CelestialObjectService : ICelestialObjectService
    {
        private readonly DataContext dataContext;

        public CelestialObjectService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public string Create(string name, string image, int positionX, int positionY)
        {
            CelestialObject celestialObject = new CelestialObject()
            {
                Name = name ,
                Image = image ,
                PositionX = positionX ,
                PositionY = positionY
            };
            try
            {
                dataContext.CelestialObjects.AddRange(celestialObject);
                dataContext.SaveChanges();

                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("an error occured to create a celestial object " + ex.Message);
            }
            return "Not OK";

        }

        public string Delete(int id)
        {

            var celestialObjects = dataContext.CelestialObjects.ToList();
            var celestialObject = celestialObjects.FirstOrDefault(u => u.Id.Equals(id));

            try
            {
                dataContext.Remove(celestialObject);
                dataContext.SaveChanges();

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error to delete a celestial object" + ex.Message);


            }
            if (celestialObject != null)
            {
                return "celestial object : " +" ' "+ celestialObject.Name +" ' "+" has been deleted";
            }
            return "not found";
        }
    

        public List<CelestialObject> GetAll()
        {
            return dataContext.CelestialObjects.ToList();
        }
    

        public string Update(int id,string name, string image, int positionX, int positionY)
        {
            var celestialObject = dataContext.CelestialObjects.FirstOrDefault(u => u.Id.Equals(id));

            celestialObject.Name = name;
            celestialObject.Image = image;
            celestialObject.PositionX = positionX;
            celestialObject.PositionY = positionY;

            try
            {
                dataContext.CelestialObjects.UpdateRange(celestialObject);
                dataContext.SaveChanges();

                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("an error occured to update a celestial object " + ex.Message);
            }
            return "Not OK";

        }
    }
}
