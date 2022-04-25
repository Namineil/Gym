using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class UserRoleResponse : BaseResponse
    {
        public UserRole UserRole {get; private set;}
        public UserRoleResponse(bool success, string message, UserRole userRole) : base(success, message)
        {
            UserRole = userRole;
        }

        public UserRoleResponse(UserRole userRole): this(true, string.Empty, userRole){}

        public UserRoleResponse(string message): this(false, message, null) {}
       
    }
}