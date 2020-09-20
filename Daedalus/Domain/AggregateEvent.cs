namespace Daedalus.Domain
{
    public abstract class AggregateEvent : IAggregateEvent
    {
        public string Name { get; }
        public int Version { get; }

        protected AggregateEvent(string name, int version)
        {
            Name = name;
            Version = version;
        }
    }
}