using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.IncubationTracking
{
    [Authorize]
    public class UIncubationModel : BasePageModel<Incubation, IncubationInputModel>

    {
        private readonly IBaseRepository<Incubation> _repo;

        public UIncubationModel(IBaseRepository<Incubation> repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
        }
        public List<Incubation> UnderIncubations { get; set; }
        public async Task OnGetAsync()
        {
            var incubations = await _repo.GetAll();
            UnderIncubations = incubations.Where(x => x.NumberOfNonViableEggs == null && x.NumberOfViableEggs == null).ToList();
        }
        public override async Task<IActionResult> OnPostUpdate(string? returnUrl = null, string? Id = null)
        {
            var rec = await _repo.GetOne(Input.Id.ToString());
            var requiredAmmountEgg = rec.NumberOfEggs - (Input.NumberOfViableEggs + Input.NumberOfNonViableEggs);
            if (requiredAmmountEgg != 0)
            {
               
                TempData["validation-message"] = "Total viable and non-viable eggs must be equal to the number of eggs.";
                return RedirectToPage();
            }
               
            await base.OnPostUpdate(returnUrl,Id);
            return RedirectToPage("./Index");
           
        }
       
    }
}
