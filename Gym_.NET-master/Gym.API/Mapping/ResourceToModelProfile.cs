using AutoMapper;
using Gym.API.Domain.Models;
using Gym.API.Resources;

namespace Shop.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAdministratorHallResource, AdministratorHall>();
            CreateMap<SaveCardResource, Card>();
            CreateMap<SaveNewsResource, News>();
            CreateMap<SaveCardSalesHistoryResource, CardSalesHistory>();
            CreateMap<SaveClientResource, Client>();
            CreateMap<SaveClassRecordsResource, ClassRecords>();
            CreateMap<SaveRoleResource, Role>();
            CreateMap<SaveRoomResource, Room>();
            CreateMap<SaveScheduleTrainingResource, ScheduleTraining>();
            CreateMap<SaveServicesCardResource, ServicesCard>();
            CreateMap<SaveSpecializationResource, Specialization>();
            CreateMap<SaveTrainerResource, Trainer>();
            CreateMap<SaveUserResource, User>();
        }
    }
}