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
    }
}
