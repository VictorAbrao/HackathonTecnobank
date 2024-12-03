using ErrorOr;
using Hackathon.AppService.Commands.Requests.Keywords;
using Hackathon.AppService.Commands.Responses.Keywords;
using Hackathon.AppService.Mappers;
using Hackathon.Domain.Services;
using Hackathon.SharedKernel.Data;
using MediatR;

namespace Hackathon.AppService.Handlers.Keywords
{
    public class ReadKeywordsCommandRequestHandler(IKeywordsService keywordsService, IUnitOfWork unitOfWork) : IRequestHandler<ReadKeywordsCommandRequest, ErrorOr<IList<ReadKeywordsCommandResponse>>>
    {
        public async Task<ErrorOr<IList<ReadKeywordsCommandResponse>>> Handle(ReadKeywordsCommandRequest request, CancellationToken ct)
        {
			var response = new List<ReadKeywordsCommandResponse>();

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
