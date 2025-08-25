using MediatR;
using Microsoft.AspNetCore.Http;

public class UploadFileAsyncCommand : IRequest<Guid>
{
    public IFormFile File{ get; set; }
}