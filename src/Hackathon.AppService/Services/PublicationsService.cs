using Dapper;
using ErrorOr;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Enums;
using System.Reflection.Metadata.Ecma335;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Services
{
    public class PublicationsService(IPublicationsRepository publicationsRepository, IUnitOfWork unitOfWork) : IPublicationsService
    {
        public async Task<ErrorOr<PublicationsEntity>> GetByDetranAsync(Detrans detran, CancellationToken ct)
        {
            var result = await publicationsRepository.GetByDetranAsync(detran, ct);           

            return result;
        }
        public async Task<ErrorOr<bool>> InsertAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            await publicationsRepository.InsertAsync(publicationEntity, ct);
            return true;
        }

        public async Task<ErrorOr<bool>> UpdateAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            await publicationsRepository.UpdateAsync(publicationEntity, ct);

            return true;
        }
    }
}
