using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Mappers;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class ReadByIdKeywordCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<ReadByIdKeywordCommandRequest, ErrorOr<ReadByIdKeywordCommandResponse>>
    {
        public async Task<ErrorOr<ReadByIdKeywordCommandResponse>> Handle(ReadByIdKeywordCommandRequest request, CancellationToken ct)
        {
            var response = new ReadByIdKeywordCommandResponse();

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
