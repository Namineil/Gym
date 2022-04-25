using System.Runtime.CompilerServices;
using System.Linq;
using System.ComponentModel;
using AutoMapper;
using Gym.API.Domain.Models;
using Gym.API.Resources;

namespace Shop.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<AdministratorHall, AdministratorHallResource>();
            CreateMap<Card, CardResource>();
            CreateMap<News, NewsResource>();
            CreateMap<CardSalesHistory, CardSalesHistoryResource>();
            CreateMap<Client, ClientResource>();
            CreateMap<ClassRecords, ClassRecordsResource>();
            CreateMap<Room, RoomResource>()
                .ForMember(dest => dest.AdministratorName, 
                           src => src.MapFrom(
                               x => x.AdministratorHall.User.FullName
                           ));
            CreateMap<ScheduleTraining, ScheduleTrainingResource>();
            CreateMap<ServicesCard, ServicesCardResource>();
            CreateMap<Specialization, SpecializationResource>();
            CreateMap<Trainer, TrainerResource>();
            CreateMap<User, UserResource>()
                .ForMember(dest => dest.Role,
                           src => src.MapFrom(
                               x => x.UserRoles
                                    .Select(
                                        y => y.Role.Name
                                    )));

            CreateMap<Role, RoleResource>();

        }
    }
}