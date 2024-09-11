using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.ViewComponents
{
    public class UpdateRoosterViewComponent:ViewComponent
    {
        private readonly IRoosterRepository _roosterRepo;
        private readonly IMapper _mapper;
        public UpdateRoosterViewComponent(
            IRoosterRepository roosterRepo,
            IMapper mapper)
        {
            _roosterRepo = roosterRepo;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var rooster = await _roosterRepo.GetOne(Id);
            var converted = _mapper.Map<RoosterInputModel>(rooster);
            return View(converted);
        }
    }
}
