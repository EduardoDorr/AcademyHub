using FluentValidation;

namespace AcademyHub.Application.Lessons.CreateLesson;

public sealed class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(r => r.Name)
            .MinimumLength(3).WithMessage("Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("Name must have a maximum of 50 characters");

        RuleFor(r => r.Description)
            .MinimumLength(3).WithMessage("Description must have a minimum of 3 characters")
            .MaximumLength(500).WithMessage("Description must have a maximum of 500 characters");

        RuleFor(r => r.VideoLink)
            .MinimumLength(3).WithMessage("VideoLink must have a minimum of 3 characters")
            .MaximumLength(200).WithMessage("VideoLink must have a maximum of 200 characters");

        RuleFor(r => r.Duration)
            .GreaterThan(0).WithMessage("Duration must be valid");
    }
}