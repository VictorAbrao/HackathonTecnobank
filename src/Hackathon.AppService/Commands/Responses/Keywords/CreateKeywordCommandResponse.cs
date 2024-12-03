
namespace Hackathon.AppService.Commands.Responses.Keywords
{
    public class CreateKeywordCommandResponse
    {
        public int Id { get; set; }

        internal void DefineId(int id) => Id = id;
    }
}
