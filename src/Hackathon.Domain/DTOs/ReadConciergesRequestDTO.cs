namespace Hackathon.Domain.DTOs
{
    public class ReadConciergesRequestDTO
    {
        public int? UF { get; set; }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public int OffSet { get; set; } = 0;
        public int Limit { get; set; } = 25;
    }
}
