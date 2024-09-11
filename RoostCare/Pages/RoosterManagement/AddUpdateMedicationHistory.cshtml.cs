using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.RoosterManagement
{
    public class AddUpdateMedicationHistoryModel : BasePageModel<MedicationHistory,MedicationHistoryInputModel>
    {
        private readonly IBaseRepository<MedicationHistory> _medicRepo;
        private readonly IMapper _mapper;
        public AddUpdateMedicationHistoryModel(IBaseRepository<MedicationHistory> repo,IMapper mapper):base(repo,mapper)
        {
            _medicRepo = repo;
            _mapper = mapper;
        }
        public int RoosterId { get; set; }
        public async Task OnGetAsync(int roosterId, int? Id = null)
        {
            RoosterId = roosterId;
            if (Id != null)
            {
                var medicationHistory = await _medicRepo.GetOne(Id.ToString());
                var converted = _mapper.Map<MedicationHistoryInputModel>(medicationHistory);
                Input = converted;
            }
        }
    }
}
