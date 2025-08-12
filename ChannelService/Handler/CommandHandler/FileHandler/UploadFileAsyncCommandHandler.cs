using MediatR;

public class UploadFileAsyncCommandHandler : IRequestHandler<UploadFileAsyncCommand, string>
{
    private readonly IFileRepository _repository;
    public UploadFileAsyncCommandHandler(IFileRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> Handle(UploadFileAsyncCommand request, CancellationToken cancellationToken)
    {
        await _repository.UploadFileAsync(request.File);
        return request.File.FileName;
    }
}