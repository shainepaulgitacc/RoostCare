using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.RoosterManagement
{
    public class AddUpdateRoosterFightHistoryModel : BasePageModel<FightHistory,FightHistoryInputModel>
    {
        private readonly IBaseRepository<FightHistory> _fightRepo;
        private readonly IMapper _mapper;
       public AddUpdateRoosterFightHistoryModel(IBaseRepository<FightHistory> repo,IMapper mapper):base(repo,mapper)
        {
            _fightRepo = repo;
            _mapper = mapper;
        }


        public int RoosterId { get; set; }
        public string? PreviousUrl { get; set; }
        public async Task OnGet(int roosterId,int? Id=null,string? prevUrl = null)
        {
            RoosterId = roosterId;
            if(Id != null)
            {
                var fightHistory = await _fightRepo.GetOne(Id.ToString());
                var converted = _mapper.Map<FightHistoryInputModel>(fightHistory);
                PreviousUrl = prevUrl;
                Input = converted;
            }
        }
    }
}
