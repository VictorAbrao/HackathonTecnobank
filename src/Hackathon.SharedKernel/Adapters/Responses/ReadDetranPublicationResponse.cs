namespace Hackathon.SharedKernel.Adapters.Responses
{
    public class ReadDetranPublicationResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Link { get; set; }
        public string PublicationType { get; set; }
        public string Content { get; set; }
        public IList<string> FilesBase64 { get; set; } = new List<string>();
    }

}
