using Hackathon.SharedKernel.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Entities
{
    public class ConciergeEntity : BaseEntity
    {
        public string ExternalId { get; set; }
        public string? ExternalLink { get; set; }
        public Detrans Detran { get; set; }
        public string Title { get; set; }
        public ConciergeStatus Status { get; set; }
        public string Body { get; set; }
        public string? Document { get; set; }
        public DateTime? Date { get; set; }
    }
}
