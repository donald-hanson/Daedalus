using System.Threading.Tasks;

namespace Daedalus.Domain
{
    public interface IAggregateRepository<TAggregate, TIdentity>
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IAggregateIdentity
    {
        Task<TAggregate> GetAsync(TIdentity id);
        Task UpdateAsync(TAggregate aggregate);
        Task CommitAsync();
    }
}