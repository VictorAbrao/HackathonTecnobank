using ErrorOr;
using Hackathon.Domain.Entities;

namespace Hackathon.Domain.Services
{
    public interface IKeywordsService 
    {
        Task<ErrorOr<int>> InsertAsync(KeywordEntity keywordEntity, CancellationToken ct);
        Task<ErrorOr<bool>> UpdateAsync(KeywordEntity keywordEntity, CancellationToken ct);
        Task<ErrorOr<KeywordEntity?>> ReadByIdAsync(int keywordId, CancellationToken ct);
        Task<ErrorOr<List<KeywordEntity>>> ReadAsync(int? wordParentId, CancellationToken ct);
        Task<ErrorOr<List<KeywordEntity>>> ReadByDetranAsync(int detran, CancellationToken ct);
        Task<ErrorOr<bool>> DeleteAsync(int keywordId, CancellationToken ct);
        Task<ErrorOr<bool>> DeleteAllByParentIdAsync(int keywordParentId, CancellationToken ct);
    }
}
