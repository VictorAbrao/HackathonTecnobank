using ErrorOr;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Adapter.DetranSP
{
    public class AdapterDetranSP : IAdapterDetran
    {
        private readonly string _baseAdressReadPublications = "";
        private readonly string _baseAdressReadPublication = "";

        public async Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            return new ReadDetranPublicationResponse();
        }

        public async Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            var response = new ReadDetranPublicationsResponse();

            response.Publications.Add(new ReadDetranPublicationResponse());
            response.Publications.Add(new ReadDetranPublicationResponse());
            response.Publications.Add(new ReadDetranPublicationResponse());

            return response;
        }
    }
}
