using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.ViewComponents
{
    public class UpdateIncubationViewComponent:ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Incubation> _repo;
        public UpdateIncubationViewComponent(
            IBaseRepository<Incubation> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var incubation = await _repo.GetOne(Id);
            if(incubation != null)
            {
                var converted = _mapper.Map<IncubationInputModel>(incubation);
                return View(converted);
            }
            return View(null);
                
        }
    }
}
