using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;

namespace ConqueringSpace.Services
{
    public class celestialObjectService : ICelestialObjectService
    {
        private readonly HttpClient httpClient;
        private List<CelestialObject> _celestialObjects = new();


        public celestialObjectService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> CreateCelestialObject(string name, string image, int positionX, int positionY)
        {
            try
            {
                CelestialObject celestialObject = new()
                {
                    Name = name,
                    Image = image,
                    PositionX = positionX,
                    PositionY = positionY
                };
                var response = await httpClient.PostAsJsonAsync("https://localhost:7116/CreateCelestialObject", celestialObject);


                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while creating celestial object: {ex.Message}");
                return "Not OK";
            }
        }

        public async Task<string> DeleteCelestialObject(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7116/Delete/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while deleting celestial object: {ex.Message}");
                return "Not OK";
            }
        }

     

        public async Task<List<CelestialObject>?> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "AllCelestialObject");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CelestialObject>>();
        }

        public async Task<CelestialObject> GetById(int id)
        {
            var celestialObjectList = await GetAll();
            foreach (var item in celestialObjectList)
            {
                if (item.Id.Equals(id))
                {
                    return item;
                }
            }
            return null;
        }
        public async Task<string> UpdateCelestialObject(int id, string name, string image, int positionX, int positionY)
        {
            try
            {
                CelestialObject celestialObject = new()
                {
                    Id = id,
                    Name = name,
                    Image = image,
                    PositionX = positionX,
                    PositionY = positionY
                };

                var response = await httpClient.PutAsJsonAsync("https://localhost:7116/Update/", celestialObject);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating celestial object: {ex.Message}");
                return "Not OK";
            }
        }

        public async Task UpdateForUser(CelestialObject celestialObject)
        {
            await Task.Run(() =>
            {
                var existingObject = _celestialObjects.FirstOrDefault(c => c.Id == celestialObject.Id);

                if (existingObject != null)
                {
                    existingObject.Name = celestialObject.Name;
                    existingObject.Image = celestialObject.Image;
                    existingObject.PositionX = celestialObject.PositionX;
                    existingObject.PositionY = celestialObject.PositionY;
                 
                }
            });
        }

  
    }  
}
