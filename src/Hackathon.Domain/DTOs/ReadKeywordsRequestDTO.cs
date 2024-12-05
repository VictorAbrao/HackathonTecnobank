namespace Hackathon.Domain.DTOs
{
    public class ReadKeywordsRequestDTO 
    {
        public int? UF { get; set; }
        public string? Word { get; set; }
        public IList<string> SubWords { get; set; }
        public int OffSet { get; set; } = 0;
        public int Limit { get; set; } = 25;
    }
}
