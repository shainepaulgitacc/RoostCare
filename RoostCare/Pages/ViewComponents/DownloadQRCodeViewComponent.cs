using LibraryManagement.Model.Infrastracture.Services;
using Microsoft.AspNetCore.Mvc;

namespace RoostCare.Pages.ViewComponents
{
    public class DownloadQRCodeViewComponent: ViewComponent
    {
        private readonly QRCode_Generator _qrGen;
        public DownloadQRCodeViewComponent(QRCode_Generator qrGen)
        {
            _qrGen = qrGen;
            
        }
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        { 
            var qrCode = _qrGen.GenerateCode(Id.ToString());
            string imagebase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCode)}";
            ViewData["qr-code"] = imagebase64;
            ViewData["filename"] = $"Rooster Id {Id}";
            return View();
        }
    }
}
