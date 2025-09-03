using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

// ---- BUILD ----
var builder = WebApplication.CreateBuilder(args);

// Controllers + FluentValidation (yeni yöntem; uyarısız)
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Kestrel: ChannelService 5200 dinlesin
builder.WebHost.ConfigureKestrel(o => o.ListenAnyIP(5200));

// JWT Auth (default scheme + Bearer)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "UserService",
            ValidAudience = "UserServiceClients",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("my_super_secret_jwt_key_1234567890!@#$%"))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor(); 


// HttpClient: Docker yok → localhost:5100 (UserService)
builder.Services.AddHttpClient("UserService", c =>
{
    c.BaseAddress = new Uri("http://localhost:5100/api/");
});

// Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new() { Title = "ChannelService API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Bearer token. Örn: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    opt.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new() { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// DbContexts
builder.Services.AddDbContext<ChannelDbContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(conn, sql => sql.EnableRetryOnFailure(3));
});
builder.Services.AddDbContext<FileDbContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("FileDb");
    opt.UseSqlServer(conn);
});

// MediatR, AutoMapper, Repo, Cache, CORS, ResponseCaching
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// İlk kurulum için tek satır (migration yoksa)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChannelDbContext>();
    await db.Database.EnsureCreatedAsync();

    var fileDb = scope.ServiceProvider.GetRequiredService<FileDbContext>();
    await fileDb.Database.EnsureCreatedAsync();
}

app.Run();
