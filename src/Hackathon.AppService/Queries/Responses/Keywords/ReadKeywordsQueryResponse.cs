using Hackathon.AppService.Queries.Responses.Concierge;

namespace Hackathon.AppService.Queries.Responses.Keywords
{
    public class ReadKeywordsQueryResponse
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<ReadKeywordsQueryItemResponse> Items { get; set; } = new();
    }

    public class ReadKeywordsQueryItemResponse
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required IList<string> SubWords { get; set; }
        public string UF { get; set; }
    }
}
