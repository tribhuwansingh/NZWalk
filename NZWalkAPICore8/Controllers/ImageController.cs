using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;
using NZWalkAPICore8.Repositaries;

namespace NZWalkAPICore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImageController : ControllerBase
    {
        private readonly IImageUploadRepositry imageUploadRepositry;
        public ImageController(IImageUploadRepositry imageUploadRepositry) 
        {
            this.imageUploadRepositry = imageUploadRepositry;
        }
        
        
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDTO imageUploadRequestDTO)
        {
            ValidateFileUpload(imageUploadRequestDTO);
            if (ModelState.IsValid) 
            {
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileName = imageUploadRequestDTO.FileName,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileSizeinBytes = imageUploadRequestDTO.File.Length
                };

                 await imageUploadRepositry.UploadImage(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var fileExts = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };
            if (!fileExts.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("fileExt", "InValid File Extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("fileSize", "File size is greater then 10MB It Should be less then 10 MB");
            }
        }
    }
}
