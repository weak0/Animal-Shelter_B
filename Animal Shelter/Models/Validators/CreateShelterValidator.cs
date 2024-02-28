using FluentValidation;

namespace Animal_Shelter.Models.Validators;

public class CreateShelterValidator : AbstractValidator<AuthShelterDto>
{
    public CreateShelterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}