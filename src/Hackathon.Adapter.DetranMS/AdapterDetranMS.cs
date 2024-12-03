using ErrorOr;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;

namespace Hackathon.Adapter.DetranMS
{
    public class AdapterDetranMS : IAdapterDetran
    {
        public async Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationRequest detranPublicationsRequest)
        {
            return new();
        }

        public async Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            return new();
        }
    }
}
