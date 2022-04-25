using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{
    public class SaveAdministratorHallResource
    {
        [Required]
        [MaxLength(150)]
        public string Status { get; set; }

        public string UserName { get; set; }
    }
}
