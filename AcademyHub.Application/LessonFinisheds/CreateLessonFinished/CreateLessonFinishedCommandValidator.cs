using FluentValidation;

namespace AcademyHub.Application.LessonFinisheds.CreateLessonFinished;

public sealed class CreateLessonFinishedCommandValidator : AbstractValidator<CreateLessonFinishedCommand>
{
    public CreateLessonFinishedCommandValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty();

        RuleFor(r => r.LessonId)
            .NotEmpty();

        RuleFor(r => r.Rating)
            .NotEmpty()
            .IsInEnum();
    }
}