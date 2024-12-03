using Dapper;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.SharedKernel.Data;

namespace Hackathon.Infra.Repository.Repositories
{
    public class ConciergeRepository(IUnitOfWork unitOfWork) : IConciergeRepository
    {
        public async Task DeleteAsync(int conciergeId, CancellationToken ct)
        {
            var sql = @$"delete from Concierge where Id = @conciergeId";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { conciergeId }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            var sql = $@"";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { conciergeEntity }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task<ConciergeEntity?> ReadAsync(int conciergeId, CancellationToken ct)
        {
            var sql = @$"select * from concierge where id = @conciergeId";

            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<ConciergeEntity?>(new CommandDefinition(sql, new { conciergeId }, cancellationToken: ct));
        }

        public async Task<List<ConciergeEntity>> ReadAsync(CancellationToken ct)
        {
            var sql = @$"select * from concierge order by Name desc";

            var result = await unitOfWork.Connection.QueryAsync<ConciergeEntity>(new CommandDefinition(sql, new { }, cancellationToken: ct));

            return result.ToList();
        }

        public async Task UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            var sql = @$"update ";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { conciergeEntity }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }
    }
}
