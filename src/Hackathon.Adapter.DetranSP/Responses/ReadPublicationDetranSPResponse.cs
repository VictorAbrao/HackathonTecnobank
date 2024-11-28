using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Adapter.DetranSP.Responses
{
    internal class ReadPublicationDetranSPResponse
    {
        public IList<ReadPublicationDetranSPItemResponse> Items { get; set; } = new List<ReadPublicationDetranSPItemResponse>();
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public int pageSize { get; set; }
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class ReadPublicationDetranSPItemResponse
    {
        public bool IsLegacy { get; set; }
        public string Id { get; set; }
        public string PublicationTypeId { get; set; }
        public string SecondLevelSectionId { get; set; }
        public string ThirdLevelSectionId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Hierarchy { get; set; }
        public int TotalTermsFound { get; set; }
    }

}
