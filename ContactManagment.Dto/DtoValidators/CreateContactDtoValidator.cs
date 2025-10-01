using FluentValidation;

namespace ContactManagment.Dto.DtoValidators
{
    public class CreateContactDtoValidator: AbstractValidator<CreateContactDto>
    {
        
            public CreateContactDtoValidator()
            {
                RuleFor(x => x.Name)
                       .NotEmpty().WithMessage("Name is required")
                       .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format");

                RuleFor(x => x.Phone)
                    .NotEmpty().WithMessage("Phone number is required")
                    .Matches(@"^[0-9]{10}$").WithMessage("Phone number must be 10 digits");

            }
        

    }
}
