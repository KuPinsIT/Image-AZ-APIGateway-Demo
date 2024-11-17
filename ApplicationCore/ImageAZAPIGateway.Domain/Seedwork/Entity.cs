using MediatR;
using System.Text.Json.Serialization;

namespace ImageAZAPIGateway.Domain.Seedwork
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public int IdentityKey { get; internal set; }

        [JsonIgnore]
        public bool IsTransient => IdentityKey == 0;

        private readonly List<INotification> _domainEvents = new();

        [JsonIgnore]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(INotification @event)
        {
            _domainEvents.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
