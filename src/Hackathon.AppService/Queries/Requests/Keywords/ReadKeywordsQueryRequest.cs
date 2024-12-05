using ErrorOr;
using Hackathon.AppService.Queries.Responses.Keywords;
using MediatR;

namespace Hackathon.AppService.Queries.Requests.Keywords
{
    public class ReadKeywordsQueryRequest : IRequest<ErrorOr<ReadKeywordsQueryResponse>>
    {
        public int? UF { get; set; }
        public string? Keyword { get; set; }
        public string? Word { get; set; }
        public IList<string>? SubWords { get; set; }
        public int OffSet { get; set; } = 0;
        public int Limit { get; set; } = 25;
    }
}
