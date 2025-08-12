using MediatR;

public class ReadFileQueryHandler : IRequestHandler<ReadFileQuery, string>
{
    private readonly IFileRepository _fileRepository;
    public ReadFileQueryHandler(IFileRepository fileRepository){
        _fileRepository = fileRepository;
    }

    public async Task<string> Handle(ReadFileQuery request, CancellationToken cancellationToken){
        return await _fileRepository.ReadFileAsync(request.FilePath);
    }
}