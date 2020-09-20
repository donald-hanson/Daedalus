namespace Daedalus.Domain
{
    public interface IAggregateEvent
    {
        int Version { get; }
        string Name { get; }
    }
}