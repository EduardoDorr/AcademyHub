using FluentValidation;

namespace AcademyHub.Application.LessonFinisheds.UpdateLessonFinished;

public sealed class UpdateLessonFinishedInputModelValidator : AbstractValidator<UpdateLessonFinishedInputModel>
{
    public UpdateLessonFinishedInputModelValidator()
    {
        RuleFor(r => r.Rating)
            .NotEmpty()
            .IsInEnum();
    }
}