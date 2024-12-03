using ErrorOr;
using Hackathon.AppService.Commands.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Commands.Requests.Keywords
{
    public class ReadByIdKeywordCommandRequest : IRequest<ErrorOr<ReadByIdKeywordCommandResponse>>
    {
        public int Id { get; set; }

        public void DefineId(int id) => Id = id;
    }
}
