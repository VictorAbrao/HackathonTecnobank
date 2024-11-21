namespace Hackathon.SharedKernel.Adapters.Responses
{
    public class ReadDetranPublicationsResponse
    {
        public DateTime LastReadPublicationDate { get; set; } = DateTime.UtcNow.AddMinutes(-10);

        public IList<ReadDetranPublicationResponse> Publications = new List<ReadDetranPublicationResponse>();
    }
}
