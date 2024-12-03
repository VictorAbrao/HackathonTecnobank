using ErrorOr;
using Hackathon.Domain.Entities;
using Hackathon.Domain.Repositories;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;

namespace Hackathon.AppService.Services
{
    public class ConciergeService(IConciergeRepository conciergeRepository, IUnitOfWork unitOfWork) : IConciergeService
    {
        public async Task<ErrorOr<bool>> DeleteAsync(int conciergeId, CancellationToken ct)
        {
            try
            {
                await unitOfWork.OpenAsync(ct);

                await unitOfWork.BeginTransactionAsync(ct);

                await conciergeRepository.DeleteAsync(conciergeId, ct);

                await unitOfWork.CommitAsync(ct);

                return true;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync(ct);
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }

        public async Task<ErrorOr<bool>> InsertAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            try
            {
                await unitOfWork.OpenAsync(ct);

                await unitOfWork.BeginTransactionAsync(ct);

                await conciergeRepository.InsertAsync(conciergeEntity, ct);

                await unitOfWork.CommitAsync(ct);

                return true;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync(ct);
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }

        public async Task<ErrorOr<ConciergeEntity?>> ReadAsync(int conciergeId, CancellationToken ct)
        {
            try
            {
                await unitOfWork.OpenAsync(ct);

                var result = await conciergeRepository.ReadAsync(conciergeId, ct);

                if (result is null)
                    return Error.NotFound("", "Concierge not found");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }

        public async Task<ErrorOr<List<ConciergeEntity>>> ReadAsync(CancellationToken ct)
        {
            try
            {
                await unitOfWork.OpenAsync(ct);

                var result = await conciergeRepository.ReadAsync(ct);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }

        public async Task<ErrorOr<bool>> UpdateAsync(ConciergeEntity conciergeEntity, CancellationToken ct)
        {
            try
            {
                await unitOfWork.OpenAsync(ct);

                await unitOfWork.BeginTransactionAsync(ct);

                await conciergeRepository.UpdateAsync(conciergeEntity, ct);

                await unitOfWork.CommitAsync(ct);

                return true;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync(ct);
                throw;
            }
            finally
            {
                await unitOfWork.CloseAsync(ct);
            }
        }
    }
}
