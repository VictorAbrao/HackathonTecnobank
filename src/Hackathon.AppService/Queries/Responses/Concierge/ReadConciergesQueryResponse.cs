using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.AppService.Queries.Responses.Concierge
{
    public class ReadConciergesQueryResponse
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<ReadConciergesQueryItemResponse> Items { get; set; } = new();
    }

    public class ReadConciergesQueryItemResponse
    {
        public int Id { get; set; }
        public string UF { get; set; }
        public string Vigency { get; set; }
        public string Type { get; set; }
        public string View { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Body { get; set; }
        public string? Document { get; set; }
    }
}
