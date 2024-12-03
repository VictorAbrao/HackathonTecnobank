using ErrorOr;
using Hackathon.Domain.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Repositories
{
    public interface IPublicationsRepository
    {
        Task<bool> InsertAsync(PublicationsEntity publicationEntity, CancellationToken ct);
        Task<bool> UpdateAsync(PublicationsEntity publicationEntity, CancellationToken ct);
        Task<PublicationsEntity> GetByDetranAsync(Detrans detran, CancellationToken ct);
    }
}
