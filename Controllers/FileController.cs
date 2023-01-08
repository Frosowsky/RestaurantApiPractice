using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace WebApplication3.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController :ControllerBase
    {
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"} )]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/Private-Files/{fileName}";

            var fileExist = System.IO.File.Exists(filePath);

            if(fileExist)
            {
                return NotFound();
            }

            var fileCategory = new FileExtensionContentTypeProvider();
            fileCategory.TryGetContentType(fileName, out var contentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents,contentType , fileName);
        }

        [HttpPost]
        public ActionResult UploadFile([FromForm]IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/Private-File/{fileName}";
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }

            return BadRequest();
        }
    }
}
