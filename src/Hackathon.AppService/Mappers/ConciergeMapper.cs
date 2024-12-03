using Hackathon.Domain.Entities;
using Hackathon.SharedKernel.Adapters.Responses;
using Hackathon.SharedKernel.Enums;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Mappers
{
    public static class ConciergeMapper
    {
        public static ConciergeEntity ToEntity(ReadDetranPublicationResponse readDetranPublicationResponse, Detrans detran) 
        {
            var response = new ConciergeEntity();

            response.ExternalId = readDetranPublicationResponse.Id;
            response.ExternalLink = readDetranPublicationResponse.Link;
            response.Title = readDetranPublicationResponse.Title;            
            response.Status = ConciergeStatus.New;
            response.Body = readDetranPublicationResponse.Content;
            response.Date = readDetranPublicationResponse.CreatedAt;
            response.Detran = detran;
            response.CreatedAt = DateTime.UtcNow;
            return response;
        }
    }
}
