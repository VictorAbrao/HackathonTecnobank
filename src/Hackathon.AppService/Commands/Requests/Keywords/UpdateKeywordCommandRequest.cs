using ErrorOr;
using Hackathon.AppService.Commands.Responses.Keywords;
using MediatR;
using System.Text.Json.Serialization;

namespace Hackathon.AppService.Commands.Requests.Keywords
{
    public class UpdateKeywordCommandRequest : IRequest<ErrorOr<UpdateKeywordCommandResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Detran { get; set; }
        public string Word { get; set; }
        public IList<string> SubWords { get; set; }
        public void DefineId(int id) => Id = id;
    }
}
