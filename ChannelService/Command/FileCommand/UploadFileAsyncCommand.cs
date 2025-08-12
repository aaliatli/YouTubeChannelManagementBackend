using MediatR;
using Microsoft.AspNetCore.Http;

public class UploadFileAsyncCommand : IRequest<string>
{
    public IFormFile File{ get; set; }
}