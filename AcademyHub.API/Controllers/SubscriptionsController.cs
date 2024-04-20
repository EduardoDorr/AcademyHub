using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using AcademyHub.Common.Auth;
using AcademyHub.Common.Results;
using AcademyHub.API.Extensions;
using AcademyHub.Application.Subscriptions.Models;
using AcademyHub.Application.Subscriptions.AddLearningTrack;
using AcademyHub.Application.Subscriptions.RemoveLearningTrack;
using AcademyHub.Application.Subscriptions.GetSubscriptions;
using AcademyHub.Application.Subscriptions.GetSubscriptionById;
using AcademyHub.Application.Subscriptions.CreateSubscription;
using AcademyHub.Application.Subscriptions.UpdateSubscription;
using AcademyHub.Application.Subscriptions.DeleteSubscription;

namespace AcademyHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _sender;

    public SubscriptionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetSubscriptionsQuery query)
    {
        var result = await _sender.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{AuthConstants.Client}, {AuthConstants.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetSubscriptionByIdQuery(id);

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
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionCommand command)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubscriptionInputModel model)
    {
        var command =
            new UpdateSubscriptionCommand(
                id,
                model.Name,
                model.Description,
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
        var result = await _sender.Send(new DeleteSubscriptionCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/add-learningTracks")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddLearningTracks(Guid id, [FromBody] IList<Guid> learningTracksId)
    {
        var command =
            new AddLearningTrackCommand(
                id,
                learningTracksId);

        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/remove-learningTracks")]
    [Authorize(Roles = AuthConstants.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveLearningTracks(Guid id, [FromBody] IList<Guid> learningTracksId)
    {
        var command =
            new RemoveLearningTrackCommand(
                id,
                learningTracksId);

        var result = await _sender.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}