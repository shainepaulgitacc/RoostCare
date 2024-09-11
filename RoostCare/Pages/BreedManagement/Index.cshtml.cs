using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.Domain;
using AutoMapper;
using RoostCare.Models.ViewModel.InputModel;
using Microsoft.AspNetCore.Authorization;

namespace RoostCare.Pages.BreedManagement
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : BasePageModel<Breed, BreedInputModel>
    {
        private readonly IBaseRepository<Breed> _breedRepo;
        private readonly IMapper _mapper;
        public IndexModel(IBaseRepository<Breed> breedRepo,IMapper mapper):base(breedRepo,mapper)
        {
            _breedRepo = breedRepo;
            _mapper = mapper;
        }
        public List<Breed> Breeds { get; set; }
        public async Task OnGetAsync()
        {
            var breeds = await _breedRepo.GetAll();
            Breeds = breeds.ToList();
        }

    }
}
