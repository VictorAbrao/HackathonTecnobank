using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.AppService.Queries.Responses.Concierge
{
    public class ReadConciergesQueryResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Body { get; set; }
        public string? Document { get; set; }
    }
}
