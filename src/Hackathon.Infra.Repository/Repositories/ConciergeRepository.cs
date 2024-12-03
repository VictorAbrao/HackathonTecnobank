﻿using Dapper;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.SharedKernel.Data;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Infra.Repository.Repositories
{
    public class ConciergeRepository(IUnitOfWork unitOfWork) : IConciergeRepository
    {
        public async Task DeleteAsync(int conciergeId, CancellationToken ct)
        {
            var sql = @$"delete from Concierge where Id = @conciergeId";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { conciergeId }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task<bool> ExistsExternalIdAsync(string externalId,Detrans detran, CancellationToken ct)
        {
            
            var sql = @$"select count(1) from Concierge with(nolock) where ExternalId = @externalId and Detran = @detran";

            var result = await unitOfWork.Connection.QueryFirstAsync<int>(new CommandDefinition(sql, new { externalId, detran = (int)detran }, transaction: unitOfWork.Transaction, cancellationToken: ct));

            return result > 0;
        }

        public async Task InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            var sql = $@"INSERT INTO Concierge
                                (ExternalId, ExternalLink, Detran, Title, Status, Body, Document, [Date], CreatedAt)
                            VALUES 
                                (@ExternalId, @ExternalLink, @Detran, @Title, @Status, @Body, @Document, @Date, @CreatedAt)";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new
            {
                conciergeEntity.ExternalId,
                conciergeEntity.ExternalLink,
                conciergeEntity.Detran,
                conciergeEntity.Title,
                conciergeEntity.Status,
                conciergeEntity.Body,
                conciergeEntity.Document,
                conciergeEntity.Date,
                conciergeEntity.CreatedAt
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));
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
