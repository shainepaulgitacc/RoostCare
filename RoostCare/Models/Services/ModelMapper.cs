using AutoMapper;
using RoostCare.Models.Domain;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Models.Services
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<BreedInputModel, Breed>().ReverseMap();
            CreateMap<RoosterInputModel, Rooster>().ReverseMap();
            CreateMap<FightHistory, FightHistoryInputModel>().ReverseMap();
            CreateMap<MedicationHistory, MedicationHistoryInputModel>().ReverseMap();
            CreateMap<Incubation, IncubationInputModel>().ReverseMap();
            CreateMap<Hatch, HatchInputModel>().ReverseMap();
            CreateMap<Income, IncomeInputModel>().ReverseMap();
            CreateMap<Expenses, ExpensesInputModel>().ReverseMap();
            CreateMap<NotificationEntity, NotificationInputModel>().ReverseMap();
            CreateMap<AccountInputModel, ApplicationUser>().ReverseMap();
        }
    }
}
