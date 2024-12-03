using ErrorOr;
using Hackathon.AppService.Commands.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Commands.Requests.Keywords
{
    public class ReadKeywordsCommandRequest : IRequest<ErrorOr<IList<ReadKeywordsCommandResponse>>>
    {
    }
}
