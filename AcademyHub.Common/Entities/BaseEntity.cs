using AcademyHub.Common.DomainEvents;

namespace AcademyHub.Common.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; protected set; }
    public bool Active { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;

        Active = true;
    }

    public virtual void Activate()
        => Active = true;

    public virtual void Deactivate()
        => Active = false;

    public virtual void SetUpdatedAtDate(DateTime updatedAtDate)
        => UpdatedAt = updatedAtDate;

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}