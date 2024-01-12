using System.ComponentModel.DataAnnotations;

namespace ConqueringSpace.Model
{
    public class UserCelestialObjectModel
    {
        public string UserId { get; set; }
        public string SelectedCelestialObjectName { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }
    }
}
