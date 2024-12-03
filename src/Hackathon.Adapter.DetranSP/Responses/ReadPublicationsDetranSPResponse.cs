using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Adapter.DetranSP.Responses
{
    internal class ReadPublicationsDetranSPResponse
    {
        public IList<ReadPublicationsDetranSPItemResponse> Items { get; set; } = new List<ReadPublicationsDetranSPItemResponse>();
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public int pageSize { get; set; }
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class ReadPublicationsDetranSPItemResponse
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


    public class ReadPublicationDetranSPResponse
    {
        public string Id { get; set; }
        public string JournalId { get; set; }
        public string SectionId { get; set; }
        public string FirstLevelSectionId { get; set; }
        public string SecondLevelSectionId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime? Date { get; set; }
        public string Journal { get; set; }
        public string Section { get; set; }
        public string FirstLevelSectionName { get; set; }
        public string SecondLevelSectionName { get; set; }
        public string PublicationType { get; set; }
        public object[] EditionPages { get; set; }
        public string Content { get; set; }
        public object[] Attachments { get; set; }
        public string AuthCode { get; set; }
    }


}
