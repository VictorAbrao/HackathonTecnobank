using ErrorOr;
using Hackathon.Domain.Entities;

namespace Hackathon.Domain.Services
{
    public interface IConciergeService
    {
        Task<ErrorOr<bool>> InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ErrorOr<bool>> UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ErrorOr<ConciergeEntity>> ReadAsync(int conciergeId, CancellationToken ct);
        Task<ErrorOr<List<ConciergeEntity>>> ReadAsync(CancellationToken ct);
        Task<ErrorOr<bool>> DeleteAsync(int conciergeId, CancellationToken ct);
    }
}
