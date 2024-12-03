using ErrorOr;
using Hackathon.AppService.Queries.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Queries.Requests.Keywords
{
    public class ReadByIdKeywordQueryRequest : IRequest<ErrorOr<ReadByIdKeywordQueryResponse>>
    {
        public int Id { get; set; }

        public void DefineId(int id) => Id = id;
    }
}
