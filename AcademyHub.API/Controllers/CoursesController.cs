using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.API.Extensions;
using AcademyHub.Application.Courses.Models;
using AcademyHub.Application.Courses.AddModule;
using AcademyHub.Application.Courses.RemoveModule;
using AcademyHub.Application.Courses.GetCourses;
using AcademyHub.Application.Courses.GetCourseById;
using AcademyHub.Application.Courses.CreateCourse;
using AcademyHub.Application.Courses.UpdateCourse;
using AcademyHub.Application.Courses.DeleteCourse;

namespace AcademyHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ISender _sender;

    public CoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetCoursesQuery query)
    {
        var result = await _sender.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCourseByIdQuery(id);

        var result = await _sender.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPost]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseInputModel model)
    {
        var command =
            new UpdateCourseCommand(
                id,
                model.Name,
                model.Description);

        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _sender.Send(new DeleteCourseCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/add-courseModules")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCourseModules(Guid id, [FromBody] IList<Guid> courseModulesId)
    {
        var command =
            new AddCourseModuleCommand(
                id,
                courseModulesId);

        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/remove-courseModules")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveCourseModules(Guid id, [FromBody] IList<Guid> courseModulesId)
    {
        var command =
            new RemoveCourseModuleCommand(
                id,
                courseModulesId);

        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}