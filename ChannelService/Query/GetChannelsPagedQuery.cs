using MediatR;

public class GetChannelsPagedQuery : IRequest<PagedResponse<Channel>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchText { get; set; }
    public string? Category { get; set; }
    public bool? IsActive{ get; set; }
}