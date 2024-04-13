using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.API.Extensions;
using AcademyHub.Application.Lessons.Models;
using AcademyHub.Application.Lessons.GetLessons;
using AcademyHub.Application.Lessons.GetLessonById;
using AcademyHub.Application.Lessons.CreateLesson;
using AcademyHub.Application.Lessons.UpdateLesson;
using AcademyHub.Application.Lessons.DeleteLesson;

namespace AcademyHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ISender _sender;

    public LessonsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetLessonsQuery query)
    {
        var result = await _sender.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetLessonByIdQuery(id);

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
    public async Task<IActionResult> Create([FromBody] CreateLessonCommand command)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLessonInputModel model)
    {
        var command =
            new UpdateLessonCommand(
                id,
                model.Name,
                model.Description,
                model.VideoLink,
                model.Duration);

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
        var result = await _sender.Send(new DeleteLessonCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}