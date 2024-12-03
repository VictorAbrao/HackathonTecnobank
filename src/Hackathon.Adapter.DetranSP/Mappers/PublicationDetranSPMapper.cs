using Hackathon.Adapter.DetranSP.Responses;
using Hackathon.SharedKernel.Adapters.Responses;

namespace Hackathon.Adapter.DetranSP.Mappers
{
    internal static class PublicationDetranSPMapper
    {
        public static ReadDetranPublicationResponse ToResponse(ReadPublicationsDetranSPItemResponse readPublicationsDetranSPItemResponse, ReadPublicationDetranSPResponse readPublicationDetranSPResponse) 
        {
            var response = new ReadDetranPublicationResponse();
            response.Id = readPublicationsDetranSPItemResponse.Id;
            response.Title = readPublicationsDetranSPItemResponse.Title;
            response.CreatedAt = readPublicationsDetranSPItemResponse.Date;
            response.Link = readPublicationsDetranSPItemResponse.Slug;
            response.Content = readPublicationDetranSPResponse.Content;
            return response;
        }
    }
}
