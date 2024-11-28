using ErrorOr;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;

namespace Hackathon.Adapter.DetranMS
{
    public class AdapterDetranMS : IAdapterDetran
    {
        public Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationRequest detranPublicationsRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            throw new NotImplementedException();
        }
    }
}
