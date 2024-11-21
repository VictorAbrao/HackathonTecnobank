using ErrorOr;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Adapters.Responses;

namespace Hackathon.Infra.ChatGPT
{
    public class AdapterChatGPT : IAdapterIA
    {
        public async Task<ErrorOr<bool>> IndexAsync(IList<ReadDetranPublicationResponse> publications, CancellationToken ct)
        {
#warning Implementar ChatGPT Adapter
            return true;
        }
    }
}
