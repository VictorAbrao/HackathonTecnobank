using Azure;
using Dapper;
using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.SharedKernel.Data;

namespace Hackathon.Infra.Repository.Repositories
{
    public class KeywordsRepository(IUnitOfWork unitOfWork) : IKeywordsRepository
    {
        public async Task DeleteAllByParentIdAsync(int keywordParentId, CancellationToken ct)
        {
            var sql = @$"delete from Keywords where WordParentId = @keywordParentId";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { keywordParentId }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task DeleteAsync(int keywordId, CancellationToken ct)
        {
            var sql = @$"delete from Keywords where Id = @keywordId";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new { keywordId }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task<int> InsertAsync(KeywordEntity keywordEntity, CancellationToken ct)
        {
            var sql = $@"insert into Keywords (WordParentId, Word, Detran, SubWords, CreatedAt) values (@WordParentId, @Word, @Detran, @SubWords, @CreatedAt); SELECT SCOPE_IDENTITY() AS Id";

            var result = await unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(sql, new
            {
                keywordEntity.Detran,
                keywordEntity.WordParentId,
                keywordEntity.Word,
                keywordEntity.CreatedAt,
                keywordEntity.SubWords,
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));

            return result;
        }

        public async Task<KeywordEntity?> ReadByIdAsync(int keywordId, CancellationToken ct)
        {
            var sql = @$"select * from Keywords with(nolock) where id = @keywordId";

            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<KeywordEntity?>(new CommandDefinition(sql, new { keywordId }, cancellationToken: ct));
        }

        public async Task<ReadKeywordsResponseDTO> ReadAsync(ReadKeywordsRequestDTO readKeywordsRequestDTO, CancellationToken ct)
        {
            var response = new ReadKeywordsResponseDTO();

            var sqlFilters = $" 1=1";

            if (readKeywordsRequestDTO.UF.HasValue)
                sqlFilters += $" AND Detran = {readKeywordsRequestDTO.UF}";

            if (!string.IsNullOrEmpty(readKeywordsRequestDTO.Word))
                sqlFilters += $" AND Word like '{readKeywordsRequestDTO.Word}%'";

            if (readKeywordsRequestDTO.SubWords.Any()) 
            {
                int subWordCounter = 0;
                sqlFilters += $"AND (";

                foreach (var subWord in readKeywordsRequestDTO.SubWords)
                {
                    sqlFilters += $"{(subWordCounter == 0 ? "" : "OR")} (Word like '%{subWord}%')";

                    subWordCounter++;
                }

                sqlFilters += $")";
            }
            var sqlCounter = @$"select count(1) from Keywords with(nolock) where {sqlFilters}";

            var sqlWithFilters = @$"select * from Keywords with(nolock) where {sqlFilters} order by Word asc                                          
                                         OFFSET @OffSet ROWS
                                         FETCH NEXT @Limit ROWS ONLY";
            
            var totalItems = await unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(sqlCounter, new { }, cancellationToken: ct));

            var items = await unitOfWork.Connection.QueryAsync<KeywordEntity>(new CommandDefinition(sqlWithFilters, new { readKeywordsRequestDTO.OffSet, readKeywordsRequestDTO.Limit }, cancellationToken: ct));

            response.TotalItems  = totalItems;
            response.Items  = items.ToList();

            return response;
        }

        public async Task UpdateAsync(KeywordEntity keywordEntity, CancellationToken ct)
        {
            var sql = @$"update Keywords Set WordParentId = @WordParentId, Word = @Word, Detran = @Detran, UpdatedAt = @UpdatedAt, SubWords = @SubWords where Id = @Id";

            await unitOfWork.Connection.ExecuteAsync(new CommandDefinition(sql, new
            {
                keywordEntity.Id,
                keywordEntity.Detran,
                keywordEntity.WordParentId,
                keywordEntity.Word,
                keywordEntity.SubWords,
                keywordEntity.UpdatedAt
            }, transaction: unitOfWork.Transaction, cancellationToken: ct));
        }

        public async Task<List<KeywordEntity>> ReadByDetranAsync(int detran, CancellationToken ct)
        {
            var sql = @$"select * from Keywords with(nolock) where Detran = @detran order by Word asc";

            var result = await unitOfWork.Connection.QueryAsync<KeywordEntity>(new CommandDefinition(sql, new { detran }, cancellationToken: ct));

            return result.ToList();
        }
    }
}
