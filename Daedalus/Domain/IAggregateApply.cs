namespace Daedalus.Domain
{
    public interface IAggregateApply<TEvent> where TEvent : IAggregateEvent
    {
        void Apply(TEvent aggregateEvent);
    }
}