using FluentValidation;

namespace Animal_Shelter.Models.Validators;

public class CreateShelterValidator : AbstractValidator<AuthShelterRegisterDto>
{
    public CreateShelterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
    }
}