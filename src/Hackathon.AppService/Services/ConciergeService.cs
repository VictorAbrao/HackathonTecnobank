using ErrorOr;
using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Services
{
    public class ConciergeService(IConciergeRepository conciergeRepository, IUnitOfWork unitOfWork) : IConciergeService
    {
        public async Task<ErrorOr<bool>> DeleteAsync(int conciergeId, CancellationToken ct)
        {
            await conciergeRepository.DeleteAsync(conciergeId, ct);
            return true;
        }

        public async Task<bool> ExistsExternalIdAsync(string externalId, Detrans detrans, CancellationToken ct)
        {
            return await conciergeRepository.ExistsExternalIdAsync(externalId, detrans, ct);
        }

        public async Task<ErrorOr<bool>> InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            await conciergeRepository.InsertAsync(conciergeEntity, ct);

            return true;
        }

        public async Task<ErrorOr<ConciergeEntity?>> ReadByIdAsync(int conciergeId, CancellationToken ct)
        {
            var result = await conciergeRepository.ReadByIdAsync(conciergeId, ct);

            if (result is null)
                return Error.NotFound("", "Concierge not found");

            return result;
        }

        public async Task<ErrorOr<ReadConciergesResponseDTO>> ReadAsync(ReadConciergesRequestDTO readConciergesRequestDTO, CancellationToken ct)
        {
            var result = await conciergeRepository.ReadAsync(readConciergesRequestDTO, ct);

            return result;
        }

        public async Task<ErrorOr<bool>> UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            await conciergeRepository.UpdateAsync(conciergeEntity, ct);
            return true;           
        }
    }
}
