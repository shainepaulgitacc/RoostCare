namespace RoostCare.Models.Services
{
    public class FileUploader
    {
        private readonly IWebHostEnvironment _env;
        public FileUploader(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string?> UploadFile(IFormFile? file, string folderName)
        {
            string fileName = null;
            if (file != null)
            {
                var fileDr = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString() +"="+file.FileName;
                string filePath = Path.Combine(fileDr, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
