using FluentValidation;

namespace AcademyHub.Application.Users.UpdateUser;

public sealed class UpdateUserInputModelValidator : AbstractValidator<UpdateUserInputModel>
{
    public UpdateUserInputModelValidator()
    {
        RuleFor(r => r.FirstName)
            .MinimumLength(3).WithMessage("First Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("First Name must have a maximum of 50 characters");

        RuleFor(r => r.FirstName)
            .MinimumLength(3).WithMessage("Last Name must have a minimum of 3 characters")
            .MaximumLength(100).WithMessage("Last Name must have a maximum of 100 characters");

        RuleFor(r => r.BirthDate)
            .LessThan(DateTime.Today).WithMessage("BirthDate must be valid");

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("Email must be valid")
            .MinimumLength(5).WithMessage("Email must have a minimum of 5 characters")
            .MaximumLength(100).WithMessage("Email must have a maximum of 100 characters");

        RuleFor(r => r.Telephone)
            .MinimumLength(10).WithMessage("Telephone must be valid")
            .MaximumLength(16).WithMessage("Email must have a maximum of 16 characters");
    }
}