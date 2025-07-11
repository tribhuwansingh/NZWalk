using NZWalkAPICore8.Model.Domain;

namespace NZWalkAPICore8.Repositaries
{
    public interface IImageUploadRepositry
    {
        Task<Image> UploadImage(Image image);
    }
}
