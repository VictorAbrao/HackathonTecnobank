using ErrorOr;
using Hackathon.AppService.Mappers;
using Hackathon.AppService.Queries.Requests.Keywords;
using Hackathon.AppService.Queries.Responses.Keywords;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class ReadByIdKeywordCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<ReadByIdKeywordQueryRequest, ErrorOr<ReadByIdKeywordQueryResponse>>
    {
        public async Task<ErrorOr<ReadByIdKeywordQueryResponse>> Handle(ReadByIdKeywordQueryRequest request, CancellationToken ct)
        {
            var response = new ReadByIdKeywordQueryResponse();

            try
            {
                await unitOfWork.OpenAsync(ct);

                var result = await keywordsService.ReadByIdAsync(request.Id, ct);

                if (result.IsError)
                    return result.Errors;

                if (result.Value is null)
                    return Error.NotFound("", "Keyword Not Found");

                if (result.Value.WordParentId is not null)
                    return Error.NotFound("", "Keyword Not Found");

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
