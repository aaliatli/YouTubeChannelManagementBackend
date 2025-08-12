using Microsoft.Extensions.Hosting; 
using System;                      
using System.IO;
using Microsoft.AspNetCore.Http;


public class FileRepository : IFileRepository
{
    private readonly string _uploadsRoot;
    public FileRepository(IHostEnvironment env){
        _uploadsRoot = Path.Combine(env.ContentRootPath, "Uploads");
        Directory.CreateDirectory(_uploadsRoot);
    }
    public async Task<string> ReadFileAsync(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }
    public async Task UploadFileAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("Geçerli bir dosya yükleyin.");

        var safeName = Path.GetFileName(file.FileName);
        var fullPath = Path.Combine(_uploadsRoot, safeName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
}