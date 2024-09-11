using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.Services;
using RoostCare.Models.ViewModel.InputModel;
using RoostCare.Models.ViewModel;

namespace RoostCare.Pages.RoosterManagement
{
	[Authorize(Roles = "Admin")]
	public class RoosterSoldOutModel : BasePageModel<Rooster, RoosterInputModel>
	{
		private readonly IRoosterRepository _roosterRepo;
		private readonly FileUploader _fileUploader;
		private readonly IMapper _mapper;
		public RoosterSoldOutModel(
			IRoosterRepository roosterRepo,
			FileUploader fileUploader,
			IMapper mapper) : base(roosterRepo, mapper)
		{
			_roosterRepo = roosterRepo;
			_fileUploader = fileUploader;
			_mapper = mapper;
		}
		public List<RoosterViewModel> Roosters { get; set; }
		public async Task OnGetAsync()
		{
			var roosters = await _roosterRepo.RoosterRecords();
			Roosters = roosters
				.GroupBy(x => x.Rooster.Id)
				.Select(f => new RoosterViewModel
				{
					Rooster = f.First().Rooster,
					Breed = f.First().Breed,
					Income = f.First().Income
				})
				.Where(x => x.Income != null && x.Rooster.RoosterCategory == RoosterCategory.ForSale)
				.ToList();
		}
	}
	
}