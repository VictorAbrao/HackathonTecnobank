using Dapper;
using ErrorOr;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using Hackathon.SharedKernel.Enums;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Services
{
    public class PublicationsService(IUnitOfWork unitOfWork) : IPublicationsService
    {
        public async Task<ErrorOr<PublicationsEntity?>> GetByDetranAsync(HackathonEnums.Detrans detran, CancellationToken ct)
        {
            var sql = $@"select * from [dbo].[Publications] where Detran = @detran";

            return unitOfWork.Connection.QueryFirstOrDefault<PublicationsEntity?>(new CommandDefinition(sql, new { detran }, cancellationToken: ct));
        }
        public async Task<ErrorOr<bool>> InsertAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            var sql = $@"insert into [dbo].[Publications] (Detran,LastReadPublications,CreatedAt) values (@detran,@lastReadPublications,@createdAt)";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new {
                detran = publicationEntity.Detran,
                lastReadPublications = publicationEntity.LastReadPublications,
                createdAt = DateTime.UtcNow,
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));

            return true;
        }

        public async Task<ErrorOr<bool>> UpdateAsync(PublicationsEntity publicationEntity, CancellationToken ct)
        {
            var sql = $@"update [dbo].[Publications] set LastReadPublications = @lastReadPublications, UpdatedAt = @updatedAt where Detran = @detran";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { 
                detran = publicationEntity.Detran, 
                lastReadPublications = publicationEntity.LastReadPublications,
                updatedAt  = DateTime.UtcNow,
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));

            return true;
        }
    }
}
