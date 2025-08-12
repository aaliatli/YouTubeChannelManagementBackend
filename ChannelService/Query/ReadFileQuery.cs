using MediatR;

public class ReadFileQuery : IRequest<string>{
    public string FilePath{get; set;}
}