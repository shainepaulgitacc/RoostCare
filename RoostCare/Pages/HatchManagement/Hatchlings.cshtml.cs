using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.HatchManagement
{
    [Authorize]
    public class HatchlingsModel : BasePageModel<Hatch, HatchInputModel>
    {
        private readonly IBaseRepository<Hatch> _hatchRepo;
        private readonly IBaseRepository<Incubation> _incubationRepo;
        public HatchlingsModel(IBaseRepository<Hatch> hatchRepo, IMapper mapper,IBaseRepository<Incubation> incubationRepo) : base(hatchRepo, mapper)
        {
            _hatchRepo = hatchRepo;
            _incubationRepo = incubationRepo;
        }

       // public List<Hatch> Hatchlings { get; set; }
        public List<HatchListViewModel> Hatchlings { get; set; }
        public async Task OnGetAsync()
        {
            var hatches = await _hatchRepo.GetAll();
            var incubations = await _incubationRepo.GetAll();


            Hatchlings = incubations
                .Join(hatches,
                inc => inc.Id,
                hatch => hatch.IncubationId,
                (inc, hatch) => new HatchListViewModel
                {
                    Incubation = inc,
                    Hatch = hatch
                }).ToList();
        }
        public override async Task<IActionResult> OnPostUpdate(string? returnUrl = null, string? Id = null)
        {
            var hatches = await _hatchRepo.GetAll();
            var incubations = await _incubationRepo.GetAll();
            var recs = incubations
                .Join(hatches,
                inc => inc.Id,
                hatch => hatch.IncubationId,
                (inc, hatch) => new
                {
                    Incubation = inc,
                    Hatch = hatch
                }).FirstOrDefault(x => x.Hatch.Id == Input.Id);
            var rec = recs?.Incubation.NumberOfViableEggs - Input.NumberOfHatchlings;
            if (rec < 0)
            {

                TempData["validation-message"] = "Number of hatchlings must be lessthan or equal to number of viable eggs.";
                return RedirectToPage();
            }
            await base.OnPostUpdate(returnUrl, Id);
            return RedirectToPage("./Index");

        }
    }
}
