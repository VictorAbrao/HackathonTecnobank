﻿using ErrorOr;
using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Services
{
    public interface IConciergeService
    {
        Task<ErrorOr<bool>> InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ErrorOr<bool>> UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct);
        Task<ErrorOr<ConciergeEntity?>> ReadByIdAsync(int conciergeId, CancellationToken ct);
        Task<ErrorOr<ReadConciergesResponseDTO>> ReadAsync(ReadConciergesRequestDTO readConciergesRequestDTO, CancellationToken ct);
        Task<ErrorOr<bool>> DeleteAsync(int conciergeId, CancellationToken ct);
        Task<bool> ExistsExternalIdAsync(string externalId, Detrans detrans, CancellationToken ct);
    }
}
