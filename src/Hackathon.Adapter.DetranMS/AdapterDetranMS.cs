using ErrorOr;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Adapter.DetranMS
{
    public class AdapterDetranMS : IAdapterDetran
    {
        public Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            throw new NotImplementedException();
        }
    }
}
