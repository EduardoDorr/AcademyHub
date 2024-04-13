using FluentValidation;

namespace AcademyHub.Application.LearningTracks.CreateLearningTrack;

public sealed class CreateLearningTrackCommandValidator : AbstractValidator<CreateLearningTrackCommand>
{
    public CreateLearningTrackCommandValidator()
    {
        RuleFor(r => r.Name)
            .MinimumLength(3).WithMessage("Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("Name must have a maximum of 50 characters");

        RuleFor(r => r.Description)
            .MinimumLength(3).WithMessage("Description must have a minimum of 3 characters")
            .MaximumLength(500).WithMessage("Description must have a maximum of 500 characters");

        RuleFor(r => r.Cover)
            .MinimumLength(3)
            .When(r => r.Cover is not null).WithMessage("Description must have a minimum of 3 characters")
            .MaximumLength(200)
            .When(r => r.Cover is not null).WithMessage("Description must have a maximum of 200 characters");
    }
}