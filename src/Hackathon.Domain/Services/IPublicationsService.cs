using ErrorOr;
using Hackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Services
{
    public interface IPublicationsService
    {
        Task<ErrorOr<bool>> InsertAsync(PublicationsEntity publicationEntity, CancellationToken ct);
        Task<ErrorOr<bool>> UpdateAsync(PublicationsEntity publicationEntity, CancellationToken ct);

        Task<ErrorOr<PublicationsEntity>> GetByDetranAsync(Detrans detran, CancellationToken ct);
    }
}
