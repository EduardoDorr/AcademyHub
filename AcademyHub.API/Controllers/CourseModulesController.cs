using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.API.Extensions;
using AcademyHub.Application.CourseModules.Models;
using AcademyHub.Application.CourseModules.GetCourseModules;
using AcademyHub.Application.CourseModules.GetCourseModuleById;
using AcademyHub.Application.CourseModules.CreateCourseModule;
using AcademyHub.Application.CourseModules.UpdateCourseModule;
using AcademyHub.Application.CourseModules.DeleteCourseModule;

namespace AcademyHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CourseModulesController : ControllerBase
{
    private readonly ISender _sender;

    public CourseModulesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseModuleViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetCourseModulesQuery query)
    {
        var result = await _sender.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseModuleDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCourseModuleByIdQuery(id);

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
    public async Task<IActionResult> Create([FromBody] CreateCourseModuleCommand command)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseModuleInputModel model)
    {
        var command =
            new UpdateCourseModuleCommand(
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
        var result = await _sender.Send(new DeleteCourseModuleCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}