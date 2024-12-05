using Hackathon.SharedKernel.Entities;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.Domain.Entities
{
    public class KeywordEntity : BaseEntity
    {
        public string Word { get; set; }
        public int? WordParentId { get; set; }
        public Detrans Detran { get; set; }
        public string SubWords { get; set; }

        public void DefineSubWords(string subwords) => SubWords = subwords;

        public void DefineWord(string word) => Word = word;

        public void DefineWordParentId(int wordParentId) => WordParentId = wordParentId;
    }
}
