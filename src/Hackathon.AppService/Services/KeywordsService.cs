using ErrorOr;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;

namespace Hackathon.AppService.Services
{
    public class KeywordsService(IKeywordsRepository repository) : IKeywordsService
    {
        public async Task<ErrorOr<bool>> DeleteAsync(int keywordId, CancellationToken ct)
        {
            await repository.DeleteAsync(keywordId, ct);
            return true;
        }

        public async Task<ErrorOr<int>> InsertAsync(KeywordEntity entity, CancellationToken ct)
        {
            return await repository.InsertAsync(entity, ct);
        }

        public async Task<ErrorOr<KeywordEntity?>> ReadByIdAsync(int keywordId, CancellationToken ct)
        {
            var result = await repository.ReadByIdAsync(keywordId, ct);

            if (result is null)
                return Error.NotFound("", "Keyword not found");

            return result;
        }

        public async Task<ErrorOr<List<KeywordEntity>>> ReadAsync(int? wordParentId, CancellationToken ct)
        {
            return await repository.ReadAsync(wordParentId, ct);
        }

        public async Task<ErrorOr<bool>> UpdateAsync(KeywordEntity entity, CancellationToken ct)
        {
            await repository.UpdateAsync(entity, ct);
            return true;
        }

        public async Task<ErrorOr<bool>> DeleteAllByParentIdAsync(int keywordParentId, CancellationToken ct)
        {
            await repository.DeleteAllByParentIdAsync(keywordParentId, ct);
            return true;
        }

        public async Task<ErrorOr<List<KeywordEntity>>> ReadByDetranAsync(int detran, CancellationToken ct)
        {
            return await repository.ReadByDetranAsync(detran, ct);
        }
    }
}
