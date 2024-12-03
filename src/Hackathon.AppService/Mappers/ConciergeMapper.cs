using ErrorOr;
using Hackathon.AppService.Queries.Responses.Concierge;
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

        internal static List<ReadConciergesQueryResponse> ToResponse(List<ConciergeEntity> conciergeEntities)
        {
            var response = new List<ReadConciergesQueryResponse>();


            foreach (var conciergeEntity in conciergeEntities)
            {
                response.Add(new ReadConciergesQueryResponse()
                {
                    Body = conciergeEntity.Body,
                    Document = conciergeEntity.Document,
                    Id = conciergeEntity.Id,
                    Status = ToStatusResponse(conciergeEntity.Status),
                    Title = conciergeEntity.Title
                });
            }

            return response;
        }

        internal static string ToStatusResponse(ConciergeStatus status)
        {
            string conciergeStatus = string.Empty;

            switch(status) 
            {
                case ConciergeStatus.New:
                    conciergeStatus = "Novo";
                    break;
                case ConciergeStatus.Approved:
                    conciergeStatus = "Aprovado";
                    break;
                case ConciergeStatus.Denied:
                    conciergeStatus = "Recusado";
                    break;
                default:
                    conciergeStatus = "Indefinido";
                    break;
            }

            return conciergeStatus;
        }
    }
}
