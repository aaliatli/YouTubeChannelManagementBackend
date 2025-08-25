using Microsoft.AspNetCore.Http;

public interface IFileRepository
{
    Task<string> ReadFileAsync(string filePath);
    Task<Guid> UploadFileAsync(FileDto dto);
     Task<List<ChannelDetailDto>> ReadChannelsFromExcelAsync(IFormFile file);
}