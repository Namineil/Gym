using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveUserRoleResource
    {
        
        public int IdRole { get; set; }

        public int IdUser { get; set; }
    }

}