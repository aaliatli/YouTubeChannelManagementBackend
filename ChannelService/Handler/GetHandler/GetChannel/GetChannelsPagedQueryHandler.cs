using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

public class GetChannelsPagedQueryHandler : IRequestHandler<GetChannelsPagedQuery, PagedResponse<Channel>>
{
    private readonly ChannelDbContext _context;

    public GetChannelsPagedQueryHandler(ChannelDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<Channel>> Handle(GetChannelsPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Channels.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.SearchText)) query = query.Where(c => c.ChannelName.Contains(request.SearchText));
        if (!string.IsNullOrWhiteSpace(request.Category)) query = query.Where(c => c.Category == request.Category);
        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }
        var data = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var totalRecords = await query.CountAsync(cancellationToken);
        return new PagedResponse<Channel>
        {
            Data = data,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords
        };
    }
}