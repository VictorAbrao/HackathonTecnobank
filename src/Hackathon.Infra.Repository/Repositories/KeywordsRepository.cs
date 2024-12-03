using Dapper;
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

        public async Task<List<KeywordEntity>> ReadAsync(int? wordParentId, CancellationToken ct)
        {
            var sql = @$"select * from Keywords with(nolock) where WordParentId {(wordParentId is null ? " is NULL" : " = @wordParentId")} order by Word asc";

            var result = await unitOfWork.Connection.QueryAsync<KeywordEntity>(new CommandDefinition(sql, new { wordParentId }, cancellationToken: ct));

            return result.ToList();
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
