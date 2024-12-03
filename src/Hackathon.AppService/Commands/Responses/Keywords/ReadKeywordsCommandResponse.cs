namespace Hackathon.AppService.Commands.Responses.Keywords
{
    public class ReadKeywordsCommandResponse
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required IList<string> SubWords { get; set; }
        public int Detran { get; set; }
    }
}
