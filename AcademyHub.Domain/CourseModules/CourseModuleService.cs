using AcademyHub.Common.Results;
using AcademyHub.Common.Results.Errors;
using AcademyHub.Domain.Lessons;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyHub.Domain.CourseModules;

public sealed class CourseModuleService : ICourseModuleService
{
    private readonly ILessonRepository _lessonRepository;

    public CourseModuleService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result> AddLessonsAsync(CourseModule courseModule, IList<Guid>? lessonsId, CancellationToken cancellationToken = default)
    {
        var lessonsResult = await GetLessonsAsync(lessonsId, cancellationToken);

        if (!lessonsResult.Success)
            return Result.Fail(lessonsResult.Errors);

        courseModule.AddLessons(lessonsResult.Value);

        return Result.Ok();
    }

    public Task<Result> RemoveLessons(CourseModule courseModule, IList<Guid> lessonsId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private async Task<Result<List<Lesson>>> GetLessonsAsync(IList<Guid>? lessonsId, CancellationToken cancellationToken)
    {
        if (lessonsId is null || lessonsId.Count == 0)
            return Result.Ok(new List<Lesson>());

        var lessons = new List<Lesson>();

        foreach (var lessonId in lessonsId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId, cancellationToken);
            lessons.Add(lesson);
        }

        if (lessons.Any(s => s is null))
        {
            var lessonsNotFound = lessonsId.Except(lessons.Where(s => s is not null).Select(s => s.Id));
            var lessonsErrors = lessonsNotFound.Select(id => new Error("LessonNotFound", $"Not found id {id}", ErrorType.NotFound));

            return Result.Fail<List<Lesson>>(lessonsErrors);
        }

        return Result.Ok(lessons.ToList());
    }
}