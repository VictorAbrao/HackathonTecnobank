using Hackathon.Adapter.DetranSP.Responses;
using Hackathon.SharedKernel.Adapters.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Adapter.DetranSP.Mappers
{
    internal static class PublicationDetranSPMapper
    {
        public static ReadDetranPublicationResponse ToResponse(ReadPublicationDetranSPItemResponse readPublicationDetranSPItemResponse) 
        {
            var response = new ReadDetranPublicationResponse();
            response.Id = readPublicationDetranSPItemResponse.Id;
            response.Title = readPublicationDetranSPItemResponse.Title;
            response.CreatedAt = readPublicationDetranSPItemResponse.Date;
            response.Link = readPublicationDetranSPItemResponse.Slug;
            return response;
        }
    }
}
