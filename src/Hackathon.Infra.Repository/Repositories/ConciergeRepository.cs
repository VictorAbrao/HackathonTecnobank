using Azure.Core;
using Dapper;
using Hackathon.Domain.DTOs;
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

        public async Task<ConciergeEntity?> ReadByIdAsync(int conciergeId, CancellationToken ct)
        {
            var sql = @$"select * from concierge where id = @conciergeId";

            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<ConciergeEntity?>(new CommandDefinition(sql, new { conciergeId }, cancellationToken: ct));
        }

        public async Task<ReadConciergesResponseDTO> ReadAsync(ReadConciergesRequestDTO readConciergesRequestDTO, CancellationToken ct)
        {
            var response = new ReadConciergesResponseDTO();

            var sqlFilters = @$" 1 = 1";

            if (!string.IsNullOrEmpty(readConciergesRequestDTO.Title))
                sqlFilters += @$" AND Title Like '{readConciergesRequestDTO.Title}%'";

            if (!string.IsNullOrEmpty(readConciergesRequestDTO.FileName))
                sqlFilters += @$" AND Document Like '%{readConciergesRequestDTO.FileName}%'";

            if (readConciergesRequestDTO.UF.HasValue)
                sqlFilters += @$" AND Detran ={readConciergesRequestDTO.UF.Value}";

            if (readConciergesRequestDTO.Status.HasValue)
                sqlFilters += @$" AND Status ={readConciergesRequestDTO.Status.Value}";

            var sqlCounter = @$"select count(1) from concierge where {sqlFilters}";

            var sqlWithFilters = @$"select * from concierge where {sqlFilters} 
                                         order by Date desc
                                         OFFSET @OffSet ROWS
                                         FETCH NEXT @Limit ROWS ONLY";

            var totalItems = await unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(sqlCounter, new { }, cancellationToken: ct));

            var items = await unitOfWork.Connection.QueryAsync<ConciergeEntity>(new CommandDefinition(sqlWithFilters, new { readConciergesRequestDTO.OffSet, readConciergesRequestDTO.Limit }, cancellationToken: ct));

            response.TotalItems = totalItems;
            response.Items = items.ToList();

            return response;
        }

        public async Task UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            var sql = @$"UPDATE Concierge
                            SET 
                                ExternalId = @ExternalId, 
                                ExternalLink = @ExternalLink, 
                                Detran = @Detran, 
                                Title = @Title, 
                                Status = @Status, 
                                Body = @Body, 
                                Document = @Document, 
                                [Date]= @Date, 
                                UpdatedAt = @UpdatedAt
                            WHERE Id=@Id";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new
            {
                conciergeEntity.Id,
                conciergeEntity.ExternalId,
                conciergeEntity.ExternalLink,
                conciergeEntity.Detran,
                conciergeEntity.Title,
                conciergeEntity.Status,
                conciergeEntity.Body,
                conciergeEntity.Document,
                conciergeEntity.Date,
                conciergeEntity.UpdatedAt
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }
    }
}
