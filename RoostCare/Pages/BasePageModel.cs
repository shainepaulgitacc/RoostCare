using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages
{
    [ValidateAntiForgeryToken]
    public class BasePageModel<T,T2>:PageModel
        where T: BaseEntity
    {
        private readonly IBaseRepository<T> _repo;
        private readonly IMapper _mapper;
        public BasePageModel(IBaseRepository<T> repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [BindProperty]
        public T2 Input { get; set; }
        public virtual async Task<IActionResult> OnPostAsync(string? returnUrl=null,string? Id=null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<T>(Input);
            await _repo.Add(converted);
            TempData["validation-message"] = "Successfully added";
            if (returnUrl != null && Id == null)
                return RedirectToPage(returnUrl);
            if (returnUrl != null && Id != null)
                return RedirectToPage(returnUrl, new { Id });
            return RedirectToPage();

        }
        public async virtual Task<IActionResult> OnPostUpdate(string? returnUrl = null, string? Id = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<T>(Input);
            await _repo.Update(converted.Id.ToString(),converted);
            TempData["validation-message"] = "Successfully updated";
            if (returnUrl != null && Id == null)
                return RedirectToPage(returnUrl);
            if (returnUrl != null && Id != null)
                return RedirectToPage(returnUrl, new { Id });
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetDelete(string? rId=null,string? Id=null,string? returnUrl=null)
        {
            try
            {
                await _repo.Delete(rId);
                TempData["validation-message"] = "Successfully deleted";
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (returnUrl != null && Id == null)
                return RedirectToPage(returnUrl);
            else if(returnUrl != null && Id != null)
                return RedirectToPage(returnUrl, new {Id});
            return RedirectToPage();
        }

        
    }
}
