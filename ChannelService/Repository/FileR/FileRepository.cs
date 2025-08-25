using Microsoft.Extensions.Hosting; 
using System;
using System.IO;
using ClosedXML.Excel;    
using Microsoft.AspNetCore.Http;
using System.Data.Common;


public class FileRepository : IFileRepository
{
    private readonly FileDbContext _context;
    public FileRepository(FileDbContext context) {
        _context = context;
    }
    public async Task<string> ReadFileAsync(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }
    public async Task<List<ChannelDetailDto>> ReadChannelsFromExcelAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Excel dosyası yok veya boş.");

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        ms.Position = 0;

        var result = new List<ChannelDetailDto>();

        using var wb = new XLWorkbook(ms);
        var ws = wb.Worksheet(1);
        var rows = ws.RowsUsed().Skip(1);

        foreach (var row in rows)
        {
            var channelName = row.Cell(1).GetString();
            var category    = row.Cell(2).GetString();

            long subscribers = 0;
            var c3 = row.Cell(3);
            if (c3.TryGetValue<long>(out var subs))
                subscribers = subs;
            else
                long.TryParse(c3.GetString(), out subscribers);

            bool isActive = false;
            var c4 = row.Cell(4);
            if (c4.TryGetValue<bool>(out var ia))
                isActive = ia;
            else
                bool.TryParse(c4.GetString(), out isActive);

            DateTime creation = DateTime.UtcNow;
            var c5 = row.Cell(5);
            if (c5.TryGetValue<DateTime>(out var dt))
                creation = dt;
            else
                DateTime.TryParse(c5.GetString(), out creation);

            result.Add(new ChannelDetailDto
            {
                ChannelId    = Guid.NewGuid(),
                ChannelName  = channelName,
                Category     = category,
                Subscribers  = subscribers,
                IsActive     = isActive,
                CreationDate = creation
            });
        }

        return result;
    }
    public async Task<Guid> UploadFileAsync(FileDto dto)
    {
        var entity = new FileEntity
        {
            Id = Guid.NewGuid(),
            FileName = dto.FileName,
            ContentType = dto.ContentType,
            Data = dto.Data,
            UploadDate = dto.UploadDate
        };

        await _context.Files.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}