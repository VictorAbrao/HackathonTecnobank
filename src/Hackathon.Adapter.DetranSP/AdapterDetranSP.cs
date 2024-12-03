using ErrorOr;
using Flurl;
using Flurl.Http;
using Hackathon.Adapter.DetranSP.Mappers;
using Hackathon.Adapter.DetranSP.Responses;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Requests;
using Hackathon.SharedKernel.Adapters.Responses;

namespace Hackathon.Adapter.DetranSP
{
    public class AdapterDetranSP : IAdapterDetran
    {
        private readonly string _baseAdress = "https://do-api-web-search.doe.sp.gov.br";

        public async Task<ErrorOr<ReadDetranPublicationResponse>> ReadPublication(ReadDetranPublicationRequest detranPublicationsRequest)
        {
            return new ReadDetranPublicationResponse();
        }

        public async Task<ErrorOr<ReadDetranPublicationsResponse>> ReadPublications(ReadDetranPublicationsRequest detranPublicationsRequest)
        {
            var response = new ReadDetranPublicationsResponse();

            try
            {
                var result = await _baseAdress
                    .AppendPathSegments("v2", "advanced-search", "publications")
                    .AppendQueryParam("periodStartingDate", detranPublicationsRequest.LastReadPublicationDate.ToString("yyyy-MM-dd"))
                    .AppendQueryParam("PageNumber", "1")
                    .AppendQueryParam("FromDate", detranPublicationsRequest.LastReadPublicationDate.ToString("yyyy-MM-dd"))
                    .AppendQueryParam("ToDate", detranPublicationsRequest.LastReadPublicationDate.AddDays(1).ToString("yyyy-MM-dd"))
                    .AppendQueryParam("PageSize", "5000")
                    .AppendQueryParam("SortField", "Date")
                    .GetJsonAsync<ReadPublicationsDetranSPResponse>();

                foreach (var item in result.Items)
                {
                    try
                    {
                        var publicationData = await ReadPublicationContentAsync(item.Slug);

                        response.Publications.Add(PublicationDetranSPMapper.ToResponse(item, publicationData));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (FlurlHttpException fhe)
            {
                var responseContent = await fhe.GetResponseStringAsync();
            }

            return response;
        }

        private async Task<ReadPublicationDetranSPResponse> ReadPublicationContentAsync(string slug)
        {
            var result = await _baseAdress
                        .AppendPathSegments("v2", "publications", slug)
                        .GetJsonAsync<ReadPublicationDetranSPResponse>();

            return result;
        }
    }
}
