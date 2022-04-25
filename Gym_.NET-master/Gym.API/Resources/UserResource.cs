using System;
using System.Collections.Generic;

namespace Gym.API.Resources
{

    public class UserResource
    {
        public int IdUser { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public string Image { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public string[] Role { get; set; }
    }

}