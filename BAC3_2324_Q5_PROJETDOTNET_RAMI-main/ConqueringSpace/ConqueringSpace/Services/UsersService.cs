using Azure.Core;
using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ConqueringSpace.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;
        
        
        public UsersService(HttpClient httpClient)
        {           
            this.httpClient = httpClient;
        }
        public async Task<List<Object>> Login(string name, string password)
        {
            string token="";
            
            //List<User> usersList;
            List<Object> usersObjectList = new();

            try
            {
                User user = new()
                {
                    Name = name,
                    Password = password
                    
                };
                var response = await httpClient.PostAsJsonAsync("https://localhost:7269/Login", user);


                response.EnsureSuccessStatusCode();

                token = await response.Content.ReadAsStringAsync();

                var usersList = await GetAll();

                User connectedUser=  usersList.FirstOrDefault(x => x.Name == name);
                usersObjectList.Add(token);  // pos 0
                usersObjectList.Add(connectedUser); // pos 1

                return usersObjectList;

            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request Exception: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging user: {ex.Message}");
            }

            return null;
        }

    
        public async Task<string> Register(string name, string password, string role)
        {
            try
            {
                User user = new()
                {
                    Name = name,
                    Password = password,
                    Role = "1"
                };
                var response = await httpClient.PostAsJsonAsync("https://localhost:7269/Register", user);


                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while registering user: {ex.Message}");
                return "Not OK";
            }
            
         
        }

        public async Task<List<User>?> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "AllUsers");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }

        public async Task<string> Update(int id, string name, string role)
        {

            try
            {
                User user = new()
                {
                    Id = id,
                    Name = name,
                    Role = role
                };

                var response = await httpClient.PutAsJsonAsync("https://localhost:7269/Update/", user);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating user : {ex.Message}");
                return "Not OK";
            }

        }

        public async Task<User> GetUserByName(string name)
        {
            var userList = await GetAll(); 
            return userList.FirstOrDefault(u => u.Name == name);
        }

        /* public async Task UpdateUserRole(User user)
         {
             // Utilisez le service pour mettre à jour le rôle de l'utilisateur
             var response = await httpClient.PostAsJsonAsync("https://localhost:7269/UpdateUserRole", user);
             response.EnsureSuccessStatusCode();
         }*/
    }
}