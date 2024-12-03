namespace Hackathon.AppService.Queries.Responses.Keywords
{
    public class ReadKeywordsQueryResponse
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required IList<string> SubWords { get; set; }
        public int Detran { get; set; }
    }
}
