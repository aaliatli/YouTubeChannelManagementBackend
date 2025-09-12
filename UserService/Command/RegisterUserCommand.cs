using System.Text.Json.Serialization;
using MediatR;

public class RegisterUserCommand : IRequest<RegisterUserRequestDto>
{
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
    [JsonIgnore]
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}