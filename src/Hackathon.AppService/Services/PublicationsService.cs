using ErrorOr;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Enums;

namespace Hackathon.AppService.Services
{
    public class PublicationsService(IUnitOfWork unitOfWork) : IPublicationsService
    {
        public async Task<ErrorOr<PublicationsEntity>> GetByDetranAsync(HackathonEnums.Detrans detran, CancellationToken ct)
        {
            return new PublicationsEntity() { CreatedAt = DateTime.UtcNow, Detran = detran, Id = Guid.NewGuid(), LastReadPublications = DateTime.UtcNow.AddDays(-1), UpdatedAt = null };

        }

        public async Task<ErrorOr<bool>> InsertAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            return true;
        }

        public async Task<ErrorOr<bool>> UpdateAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            return true;
        }
    }
}
