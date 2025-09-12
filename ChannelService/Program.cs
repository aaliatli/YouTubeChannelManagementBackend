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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.WebHost.ConfigureKestrel(o => o.ListenAnyIP(5200));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddHttpClient("UserService", c =>
{
    c.BaseAddress = new Uri("http://localhost:5100/api/");
});

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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole(); 
});

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
app.UseCors("ReactApp");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChannelDbContext>();
    await db.Database.EnsureCreatedAsync();

    var fileDb = scope.ServiceProvider.GetRequiredService<FileDbContext>();
    await fileDb.Database.EnsureCreatedAsync();
}

app.Run();
