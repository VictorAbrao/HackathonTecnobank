using Hackathon.SharedKernel.Adapters;
using static Hackathon.SharedKernel.Enums.HackathonEnums;

namespace Hackathon.SharedKernel.Factories
{
    public interface IDetranAdapterFactory
    {
        IAdapterDetran GetAdapterInstance(Detrans detrans);
    }
}
