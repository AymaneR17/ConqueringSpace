using ApiConqueringSpace.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiConqueringSpace.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<CelestialObject> CelestialObjects { get; set; }



    }
}
