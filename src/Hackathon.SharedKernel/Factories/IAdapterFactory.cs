using Hackathon.SharedKernel.Adapters;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.SharedKernel.Factories
{
    public interface IAdapterFactory
    {
        IAdapterDetran GetAdapterInstance(Detrans detrans);
    }
}
