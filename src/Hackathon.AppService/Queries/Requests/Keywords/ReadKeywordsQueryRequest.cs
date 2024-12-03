using ErrorOr;
using Hackathon.AppService.Queries.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Queries.Requests.Keywords
{
    public class ReadKeywordsQueryRequest : IRequest<ErrorOr<IList<ReadKeywordsQueryResponse>>>
    {
    }
}
