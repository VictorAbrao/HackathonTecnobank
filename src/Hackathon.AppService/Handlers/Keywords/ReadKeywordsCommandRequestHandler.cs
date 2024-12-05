using ErrorOr;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Queries.Requests.Keywords;
using Hackathon.AppService.Queries.Responses.Keywords;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class ReadKeywordsCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<ReadKeywordsQueryRequest, ErrorOr<ReadKeywordsQueryResponse>>
    {
        public async Task<ErrorOr<ReadKeywordsQueryResponse>> Handle(ReadKeywordsQueryRequest request, CancellationToken ct)
        {
			var response = new ReadKeywordsQueryResponse();

			try
			{
                var requestDto = KeywordsMapper.ToDto(request);

                await unitOfWork.OpenAsync(ct);

                var result = await keywordsService.ReadAsync(requestDto, ct);

                if (result.IsError)
                    return result.Errors;

                response = KeywordsMapper.ToResponse(requestDto, result.Value);
            }
			catch (Exception ex)
			{
                Error.Failure("", ex.Message);
			}
			finally 
			{
				await unitOfWork.CloseAsync(ct);
            }

            return response;
        }
    }
}
