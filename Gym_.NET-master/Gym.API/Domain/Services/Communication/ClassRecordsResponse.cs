using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class ClassRecordsResponse : BaseResponse
    {
        public ClassRecords ClassRecords {get; private set;}
        public ClassRecordsResponse(bool success, string message, ClassRecords classRecords) : base(success, message)
        {
            ClassRecords = classRecords;
        }

        public ClassRecordsResponse(ClassRecords classRecords): this(true, string.Empty, classRecords){}

        public ClassRecordsResponse(string message): this(false, message, null) {}
       
    }
}