using ErrorOr;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Queries.Requests.Keywords;
using Hackathon.AppService.Queries.Responses.Keywords;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class ReadKeywordsCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<ReadKeywordsQueryRequest, ErrorOr<IList<ReadKeywordsQueryResponse>>>
    {
        public async Task<ErrorOr<IList<ReadKeywordsQueryResponse>>> Handle(ReadKeywordsQueryRequest request, CancellationToken ct)
        {
			var response = new List<ReadKeywordsQueryResponse>();

			try
			{
                await unitOfWork.OpenAsync(ct);

                var result = await keywordsService.ReadAsync(null, ct);

                if (result.IsError)
                    return result.Errors;

                response = KeywordsMapper.ToResponse(result.Value);
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
