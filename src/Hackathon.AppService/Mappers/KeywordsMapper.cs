using System.ComponentModel.DataAnnotations;
using Azure.Core;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Queries.Requests.Keywords;
using Hackathon.AppService.Queries.Responses.Keywords;
using Hackathon.Domain.DTOs;
using Hackathon.Domain.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.AppService.Mappers
{
    public static class KeywordsMapper
    {
        internal static KeywordEntity ToEntity(CreateKeywordCommandRequest request)
        {
            var entity = new KeywordEntity();
            entity.Word = request.Word;
            entity.Detran = ToDetranEnum(request.UF);
            entity.CreatedAt = DateTime.UtcNow;

            if (request.SubWords is not null)
                entity.SubWords = ToSubWords(request.SubWords);

            return entity;
        }

        internal static KeywordEntity ToEntity(UpdateKeywordCommandRequest request, KeywordEntity entity)
        {
            entity.Word = request.Word;
            entity.Detran = ToDetranEnum(request.UF);
            entity.UpdatedAt = DateTime.UtcNow;

            if (request.SubWords is not null)
                entity.SubWords = ToSubWords(request.SubWords);
            return entity;
        }

        internal static string ToSubWords(IList<string> subWords)
            => string.Join(",", subWords
                .Select(s => s.Trim())
                .OrderBy(o => o)
                .ToList());

        internal static string[] ToSubWords(string subWords)
            => subWords.Length > 0 ? subWords.Split(',').OrderBy(o => o).ToArray() : [];
        internal static ReadKeywordsQueryResponse ToResponse(ReadKeywordsRequestDTO readKeywordsRequestDTO, ReadKeywordsResponseDTO  readKeywordsResponseDTO) 
        {
            var response = new ReadKeywordsQueryResponse();
            response.TotalItems = readKeywordsResponseDTO.TotalItems;
            response.TotalPages = (int)Math.Ceiling((double)readKeywordsResponseDTO.TotalItems / readKeywordsRequestDTO.Limit);

            foreach (var keywordEntity in readKeywordsResponseDTO.Items)
            {
                response.Items.Add(new ReadKeywordsQueryItemResponse()
                {
                    Word = keywordEntity.Word,
                    UF = ToDetranName(keywordEntity.Detran),
                    Id = keywordEntity.Id,
                    SubWords = ToSubWords(keywordEntity.SubWords),                    
                });
            }

            return response;
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

        internal static Detrans ToDetranEnum(string detran)
        {
            Detrans detranEnum = Detrans.SP;

            switch (detran.Trim().ToUpper())
            {
                case "SP":
                    detranEnum = Detrans.SP;
                    break;
                case "MS":
                    detranEnum = Detrans.MS;
                    break;
            }

            return detranEnum;
        }


        internal static ReadByIdKeywordQueryResponse ToResponse(KeywordEntity keywordEntity)
        {
            var response = new ReadByIdKeywordQueryResponse();
            response.Word = keywordEntity.Word;
            response.UF =ToDetranName(keywordEntity.Detran);
            response.Id = keywordEntity.Id;
            response.Subwords = ToSubWords(keywordEntity.SubWords);
            return response;
        }

        internal static ReadKeywordsRequestDTO ToDto(ReadKeywordsQueryRequest request)
        {
            var response = new ReadKeywordsRequestDTO();

            response.UF = request.UF;
            response.Word = request.Word;
            response.OffSet = request.OffSet;
            response.Limit = request.Limit;

            if (request.SubWords is not null)
                response.SubWords = request.SubWords;
            else
                response.SubWords = new List<string>();

            return response;
        }
    }
}
