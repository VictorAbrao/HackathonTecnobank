using ErrorOr;
using Hackathon.SharedKernel.Adapters.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.SharedKernel.Adapters
{
    public interface IAdapterIA
    {
        Task<ErrorOr<bool>> IndexAsync(ReadDetranPublicationResponse publication, CancellationToken ct);
        Task<ErrorOr<bool>> IndexAsync(IList<ReadDetranPublicationResponse> publications, CancellationToken ct);
    }
}
