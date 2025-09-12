using FluentValidation;

public class CreateChannelValidator : AbstractValidator<CreateChannelDto>
{
    public CreateChannelValidator()
    {
        RuleFor(x => x.ChannelName).NotEmpty().WithMessage("Kanal adı boş bırakılamaz.").MaximumLength(50).WithMessage("Kanal adı 50 karakterden fazla olamaz").MinimumLength(3).WithMessage("Kanal adı 3 karakterden az olamaz.");
        RuleFor(x=> x.Category).NotEmpty().WithMessage("Kanal adı boş bırakılamaz.").MaximumLength(50).WithMessage("Kategori adı 50 karakterden fazla olamaz").MinimumLength(3).WithMessage("Kategori adı 3 karakterden az olamaz.");
    }
}