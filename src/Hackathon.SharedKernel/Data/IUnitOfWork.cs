using System.Data.Common;

namespace Hackathon.SharedKernel.Data
{
    public interface IUnitOfWork
    {
        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; }
        Task OpenAsync(CancellationToken ct);
        Task CloseAsync(CancellationToken ct);
        Task BeginTransactionAsync(CancellationToken ct);
        Task CommitAsync(CancellationToken ct);
        Task RollbackAsync(CancellationToken ct);
    }
}
