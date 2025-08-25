using System.Net.Mime;
using MediatR;

public class UploadFileAsyncCommandHandler : IRequestHandler<UploadFileAsyncCommand, Guid>
{
    private readonly IFileRepository _repository;
    public UploadFileAsyncCommandHandler(IFileRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(UploadFileAsyncCommand request, CancellationToken cancellationToken)
    {
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream()) {
            await request.File.CopyToAsync(memoryStream, cancellationToken);
            fileBytes = memoryStream.ToArray();
        }
        var fileDto = new FileDto
        {
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            Data = fileBytes,
            UploadDate = DateTime.UtcNow
        };
        var fileId = await _repository.UploadFileAsync(fileDto);
        return fileId;
    }
}