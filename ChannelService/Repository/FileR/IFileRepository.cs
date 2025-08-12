using Microsoft.AspNetCore.Http;

public interface IFileRepository
{
    Task<string> ReadFileAsync(string filePath);
    Task UploadFileAsync(IFormFile file);
}