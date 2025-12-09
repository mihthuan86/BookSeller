namespace BookSeller.Data.Service
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string path);
    }
}
