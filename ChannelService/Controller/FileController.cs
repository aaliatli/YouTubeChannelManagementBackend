using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;
    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload-file")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadFileAsyncCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new {FileName = result});
    }

    [HttpGet("read-file")]
    public async Task<IActionResult> Read([FromQuery] string filePath){
        var content = await _mediator.Send(new ReadFileQuery{FilePath = filePath});
        return Ok(content);
    }

}
