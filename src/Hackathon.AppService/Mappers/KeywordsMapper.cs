using Azure.Core;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
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
            entity.Detran = request.Detran;
            entity.CreatedAt = DateTime.UtcNow;

            if (request.SubWords is not null)
                entity.SubWords = ToSubWords(request.SubWords);

            return entity;
        }

        internal static KeywordEntity ToEntity(UpdateKeywordCommandRequest request, KeywordEntity entity)
        {
            entity.Word = request.Word;
            entity.Detran = request.Detran;
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
        internal static List<ReadKeywordsCommandResponse> ToResponse(IList<KeywordEntity> keywordEntities) 
        {
            var response = new List<ReadKeywordsCommandResponse>();

            foreach (var keywordEntity in keywordEntities)
            {
                response.Add(new ReadKeywordsCommandResponse()
                {
                    Word = keywordEntity.Word,
                    Detran = keywordEntity.Detran,
                    Id = keywordEntity.Id,
                    SubWords = ToSubWords(keywordEntity.SubWords)
                });
            }

            return response;
        }

        internal static ReadByIdKeywordCommandResponse ToResponse(KeywordEntity keywordEntity)
        {
            var response = new ReadByIdKeywordCommandResponse();
            response.Word = keywordEntity.Word;
            response.Detran = keywordEntity.Detran;
            response.Id = keywordEntity.Id;
            response.Subwords = ToSubWords(keywordEntity.SubWords);
            return response;
        }
    }
}
