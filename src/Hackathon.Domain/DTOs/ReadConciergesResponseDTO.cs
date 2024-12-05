using Hackathon.Domain.Entities;

namespace Hackathon.Domain.DTOs
{
    public class ReadConciergesResponseDTO 
    {
        public int TotalItems { get; set; }
        public List<ConciergeEntity> Items { get; set; }
    }
}
