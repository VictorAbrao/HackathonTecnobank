using Hackathon.Domain.Entities;

namespace Hackathon.Domain.DTOs
{
    public class ReadKeywordsResponseDTO
    {    
        public int TotalItems { get; set; }
        public List<KeywordEntity> Items { get; set; } = new();

    }
}
