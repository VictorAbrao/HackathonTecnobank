using System.Text.Json.Serialization;

namespace Hackathon.AppService.Queries.Responses.Keywords
{
    public class ReadByIdKeywordQueryResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Word { get; set; }
        public int Detran { get; set; }
        public string[] Subwords { get; set; }
        public void DefineId(int id) => Id = id;
    }
}
