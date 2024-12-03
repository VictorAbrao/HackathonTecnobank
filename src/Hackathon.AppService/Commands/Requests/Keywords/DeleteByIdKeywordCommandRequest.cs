using ErrorOr;
using Hackathon.AppService.Commands.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Commands.Requests.Keywords
{
    public class DeleteByIdKeywordCommandRequest : IRequest<ErrorOr<DeleteByIdKeywordCommandResponse>>
    {
        public int Id { get; set; }

        public void DefineId(int id) => Id = id;
    }
}
