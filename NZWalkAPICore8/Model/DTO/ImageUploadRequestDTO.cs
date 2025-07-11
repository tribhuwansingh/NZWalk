using System.ComponentModel.DataAnnotations;

namespace NZWalkAPICore8.Model.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
        [Required]
        public string FileName { get; set; }

    }
}
