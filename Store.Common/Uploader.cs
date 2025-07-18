using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace GharareSabz.Common
{
    public class Uploader
    {
        private readonly IHostingEnvironment _environment;

        public Uploader(IHostingEnvironment Environment)
        {
            _environment = Environment;
        }

        public UploadDto UploadFile(IFormFile file, string folder)
        {
            if (file != null)
            {
                // var folder = "/Images/UserImages/";
                folder = folder.TrimStart('/').Replace("\\", "/");
                var uploadRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadRootFolder)) Directory.CreateDirectory(uploadRootFolder);

                if (file == null || file.Length == 0)
                    return new UploadDto
                    {
                        Status = false,
                        FileNameAddress = ""
                    };

                var fileName = DateAndTime.Now.Ticks + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto
                {
                    FileNameAddress = "/" + folder + fileName,
                    Status = true
                };
            }

            return null;
        }

        public class UploadDto
        {
            public int Id { get; set; }
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
        }
    }
}