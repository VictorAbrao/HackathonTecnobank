namespace Hackathon.AppService.Commands.Responses.Keywords
{
    public class DeleteByIdKeywordCommandResponse
    {
        public int Id { get; set; }
        public void DefineId(int id) => Id = id;
    }
}
