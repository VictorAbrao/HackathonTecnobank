using ErrorOr;
using Hackathon.AppService.Commands.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Commands.Requests.Keywords
{
    public class CreateKeywordCommandRequest : IRequest<ErrorOr<CreateKeywordCommandResponse>>
    {
        public int Detran { get; set; }
        public required string Word { get; set; }
        public required IList<string> SubWords { get; set; }
    }
}
