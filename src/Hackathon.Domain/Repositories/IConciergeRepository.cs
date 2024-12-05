using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Repositories
{
    public interface IConciergeRepository
    {
        Task InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ConciergeEntity?> ReadByIdAsync(int conciergeId, CancellationToken ct);
        Task<ReadConciergesResponseDTO> ReadAsync(ReadConciergesRequestDTO readConciergesRequestDTO, CancellationToken ct);
        Task DeleteAsync(int conciergeId, CancellationToken ct);
        Task<bool> ExistsExternalIdAsync(string externalId, Detrans detrans, CancellationToken ct);
    }
}
