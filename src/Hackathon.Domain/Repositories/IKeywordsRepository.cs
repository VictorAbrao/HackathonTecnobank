﻿using ErrorOr;
using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;

namespace Hackathon.Domain.Repositories
{
    public interface IKeywordsRepository
    {
        Task<int> InsertAsync(KeywordEntity keywordEntity, CancellationToken ct);
        Task UpdateAsync(KeywordEntity keywordEntity, CancellationToken ct);
        Task<KeywordEntity?> ReadByIdAsync(int keywordId, CancellationToken ct);
        Task<ReadKeywordsResponseDTO> ReadAsync(ReadKeywordsRequestDTO readKeywordsRequestDTO, CancellationToken ct);
        Task<List<KeywordEntity>> ReadByDetranAsync(int detran, CancellationToken ct);
        Task DeleteAsync(int keywordId, CancellationToken ct);
        Task DeleteAllByParentIdAsync(int keywordParentId, CancellationToken ct);
    }
}
