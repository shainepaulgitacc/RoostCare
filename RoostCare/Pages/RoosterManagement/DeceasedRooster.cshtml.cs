using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.Services;
using RoostCare.Models.ViewModel;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.RoosterManagement
{
    public class DeceasedRoosterModel : BasePageModel<Rooster, RoosterInputModel>
    {
        private readonly IRoosterRepository _roosterRepo;
        private readonly FileUploader _fileUploader;
        private readonly IBaseRepository<FightHistory> _fhRepo;
        private readonly IMapper _mapper;
        public DeceasedRoosterModel(
            IRoosterRepository roosterRepo,
            FileUploader fileUploader,
            IMapper mapper,
            IBaseRepository<FightHistory> fhRepo) : base(roosterRepo, mapper)
        {
            _roosterRepo = roosterRepo;
            _fileUploader = fileUploader;
            _mapper = mapper;
            _fhRepo = fhRepo;
        }
        public List<RoosterViewModel> Roosters { get; set; }
        public async Task OnGetAsync()
        {
            var roosters = await _roosterRepo.RoosterRecords();
            var fightHistories = await _fhRepo.GetAll();
            Roosters = roosters
                .GroupBy(x => x.Rooster.Id)
                .Select(f => new RoosterViewModel
                {
                    Rooster = f.First().Rooster,
                    Breed = f.First().Breed,
                    Income = f.First().Income,
                    HasLoose = fightHistories.Where(x => x.RoosterId == f.Key).Any(x => x.FightResult == FightResult.Loss)
                })
                .Where(x => x.HasLoose).ToList();
        }
    }
}
