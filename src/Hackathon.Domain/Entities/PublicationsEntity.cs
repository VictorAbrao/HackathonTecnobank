using Hackathon.SharedKernel.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Entities
{
    public class PublicationsEntity : BaseEntity
    {
        public DateTime? LastReadPublications { get; set; }

        public Detrans Detran { get; set; }

    }
}
