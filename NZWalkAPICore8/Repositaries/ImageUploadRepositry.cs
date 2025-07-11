using NZWalkAPICore8.Data;
using NZWalkAPICore8.Model.Domain;

namespace NZWalkAPICore8.Repositaries
{
    public class ImageUploadRepositry : IImageUploadRepositry
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalkDBContext dbContext;
        public ImageUploadRepositry(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,NZWalkDBContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;  
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localImgPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}" );
            //Save the Image to Local path
            using var stream = new FileStream(localImgPath, FileMode.CreateNew);
            await image.File.CopyToAsync(stream);

        //https://localhost:7209/api/Images/abc.jpg
        var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
         image.FilePath = urlFilePath;

            //Save the image to Images Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
