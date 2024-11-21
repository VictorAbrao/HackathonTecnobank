using Hackathon.SharedKernel.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Hackathon.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection;
        private DbTransaction _transaction;
        private IConfiguration _configuration;

        public DbConnection Connection { get { return _connection; } }
        public DbTransaction Transaction { get { return _transaction; } }

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task BeginTransactionAsync(CancellationToken ct)
        {
            _transaction = await _connection.BeginTransactionAsync(ct);
        }

        public async Task CloseAsync(CancellationToken ct)
        {
            await _connection.CloseAsync();
        }

        public async Task CommitAsync(CancellationToken ct)
        {
            if (_transaction is null)
                return;

            await _transaction.CommitAsync(ct);
        }

        public async Task OpenAsync(CancellationToken ct)
        {
            if (_connection is null)
            {
                _connection = new SqlConnection(_configuration.GetConnectionString("Hackathon"));
                await _connection.OpenAsync(ct);
            }
            else
                throw new ApplicationException("Connection Already Opened");

        }

        public async Task RollbackAsync(CancellationToken ct)
        {
            if (_transaction is null)
                return;

            await _transaction.RollbackAsync(ct);
        }
    }
}
