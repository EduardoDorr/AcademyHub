﻿using FluentValidation;

namespace AcademyHub.Application.Courses.UpdateCourse;

public sealed class UpdateCourseInputModelValidator : AbstractValidator<UpdateCourseInputModel>
{
    public UpdateCourseInputModelValidator()
    {
        RuleFor(r => r.Name)
            .MinimumLength(3).WithMessage("Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("Name must have a maximum of 50 characters");

        RuleFor(r => r.Description)
            .MinimumLength(3).WithMessage("Description must have a minimum of 3 characters")
            .MaximumLength(500).WithMessage("Description must have a maximum of 500 characters");
    }
}