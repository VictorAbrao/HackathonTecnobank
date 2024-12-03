using System.Text.Json.Serialization;

namespace Hackathon.AppService.Commands.Responses.Keywords
{
    public class ReadByIdKeywordCommandResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Word { get; set; }
        public int Detran { get; set; }
        public string[] Subwords { get; set; }
        public void DefineId(int id) => Id = id;
    }
}
