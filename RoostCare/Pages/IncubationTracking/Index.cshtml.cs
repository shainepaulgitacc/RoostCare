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
    public class IndexModel : BasePageModel<Incubation, IncubationInputModel>
    {
        private readonly IBaseRepository<Incubation> _repo;

        public IndexModel(IBaseRepository<Incubation> repo,IMapper mapper):base(repo,mapper)
        {
            _repo = repo;
        }
        public List<Incubation> IncubationTrackers { get; set; }
        public async Task OnGetAsync()
        {
             var incubations = await _repo.GetAll();
             IncubationTrackers = incubations.ToList();
        }
    }
}
