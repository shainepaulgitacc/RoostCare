using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.ViewComponents
{
    public class HatchCompleteViewComponent:ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Hatch> _repo;
        public HatchCompleteViewComponent(
            IBaseRepository<Hatch> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var hatches = await _repo.GetOne(Id);
            if (hatches != null)
            {
                var converted = _mapper.Map<HatchInputModel>(hatches);
                return View(converted);
            }
            return View(null);

        }
    }
}
