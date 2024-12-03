using Hackathon.Domain.Entities;

namespace Hackathon.Domain.Repositories
{
    public interface IConciergeRepository
    {
        Task InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ConciergeEntity?> ReadAsync(int conciergeId, CancellationToken ct);
        Task<List<ConciergeEntity>> ReadAsync(CancellationToken ct);
        Task DeleteAsync(int conciergeId, CancellationToken ct);
    }
}
