using ErrorOr;
using Hackathon.AppService.Queries.Requests.Concierge;
using Hackathon.AppService.Queries.Responses.Concierge;
using Hackathon.Domain.DTOs;
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

        internal static ReadConciergesQueryResponse ToResponse(ReadConciergesRequestDTO requestDTO, ReadConciergesResponseDTO  responseDTO )
        {
            var response = new ReadConciergesQueryResponse();

            response.TotalItems = responseDTO.TotalItems;
            response.TotalPages = (int)Math.Ceiling((double)responseDTO.TotalItems / requestDTO.Limit);

            foreach (var conciergeEntity in responseDTO.Items)
            {
                response.Items.Add(new ReadConciergesQueryItemResponse()
                {
                    Body = conciergeEntity.Body,
                    Document = conciergeEntity.Document,
                    Id = conciergeEntity.Id,
                    Status = ToStatusResponse(conciergeEntity.Status),
                    Vigency = "-",
                    Title = conciergeEntity.Title,
                    View = "Estadual",
                    Type = "-",
                    UF = ToDetranName(conciergeEntity.Detran)
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
                    conciergeStatus = "Em análise";
                    break;
                case ConciergeStatus.Approved:
                    conciergeStatus = "Aprovado";
                    break;
                case ConciergeStatus.Denied:
                    conciergeStatus = "Reprovado";
                    break;
                default:
                    conciergeStatus = "Indefinido";
                    break;
            }

            return conciergeStatus;
        }

        internal static string ToDetranName(Detrans detran)
        {
            string detranName = string.Empty;

            switch (detran)
            {
                case Detrans.SP:
                    detranName = "SP";
                    break;
                case Detrans.MS:
                    detranName = "MS";
                    break;
                default:
                    detranName = "-";
                    break;
            }

            return detranName;
        }

        internal static ReadConciergesRequestDTO ToDto(ReadConciergesQueryRequest request) 
        {
            var offset = request.PageIndex <= 0 ? 0 : request.PageIndex * request.PageSize;

            var dto = new ReadConciergesRequestDTO();
            dto.UF = request.UF;
            dto.Status = request.Status;
            dto.Title = request.Title;
            dto.FileName = request.FileName;
            dto.OffSet = offset;
            dto.Limit = request.PageSize;
            return dto;
        }
    }
}
