using MediatR;

namespace AcademyHub.Common.DomainEvents;

public interface IDomainEventHandler<TDomainEvent>
    : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
}