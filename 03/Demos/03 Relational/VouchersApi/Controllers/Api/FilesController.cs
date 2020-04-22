using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vouchers.Controller.Api {
    [Route ("api/[controller]")]
    public class FilesController : Microsoft.AspNetCore.Mvc.Controller {
        //[HttpPost, DisableRequestSizeLimit, Route("api/files")]
        //public async Task Upload(IFormFile file)
        //{            
        //    //your file stream
        //    var stream = file.OpenReadStream();
        //}

        private readonly IWebHostEnvironment env;
        public FilesController (IWebHostEnvironment environment) {
            env = environment;
        }

        //[Route("api/files")]
        [HttpPost, DisableRequestSizeLimit] //[HttpPost, DisableRequestSizeLimit, Route("api/files")]
        [Consumes ("multipart/form-data")]
        public async Task Post (IFormFile File) {
            //TODO: save the ticket ... get id

            var files = Request.Form.Files;

            var uploadsRootFolder = Path.Combine (env.WebRootPath, "uploads");
            if (!Directory.Exists (uploadsRootFolder)) {
                Directory.CreateDirectory (uploadsRootFolder);
            }

            foreach (var file in files) {
                //TODO: do security checks ...!

                if (file == null || file.Length == 0) {
                    continue;
                }

                var filePath = Path.Combine (uploadsRootFolder, file.FileName);
                using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                    await file.CopyToAsync (fileStream).ConfigureAwait (false);
                }
            }
        }

    }
}