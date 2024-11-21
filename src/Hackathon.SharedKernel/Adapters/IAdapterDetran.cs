using ErrorOr;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.SharedKernel.Adapters
{
    public interface IAdapterDetran
    {
        Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest);
        Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationsRequest detranPublicationsRequest);
    }
}
