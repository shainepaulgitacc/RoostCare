using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;
using System.Runtime.CompilerServices;

namespace RoostCare.Pages.HatchManagement
{
    [Authorize]
    public class IndexModel : BasePageModel<Hatch,HatchInputModel>
    {
        private readonly IBaseRepository<Hatch> _hatchRepo;
        public IndexModel(IBaseRepository<Hatch> hatchRepo,IMapper mapper):base(hatchRepo,mapper)
        {
            _hatchRepo = hatchRepo;
        }

        public List<Hatch> Hatches { get; set; }
        public async Task OnGetAsync()
        {
            var hatches = await _hatchRepo.GetAll();
            Hatches = hatches.ToList();
        }
    }
}
