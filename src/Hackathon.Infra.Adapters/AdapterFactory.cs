using Hackathon.Adapter.DetranMS;
using Hackathon.Adapter.DetranSP;
using Hackathon.SharedKernel.Adapters;
using Hackathon.SharedKernel.Enums;
using Hackathon.SharedKernel.Factories;

namespace Hackathon.Infra.Adapters
{
    public class AdapterFactory : IAdapterFactory
    {
        public IAdapterDetran GetAdapterInstance(HackathonEnums.Detrans detrans)
        {
            switch (detrans) 
            {
                case HackathonEnums.Detrans.MS:
                    return new AdapterDetranMS();

                case HackathonEnums.Detrans.SP:
                    return new AdapterDetranSP();

                default:
                    throw new NotImplementedException("Detran Não Implementado");
            }            
        }
    }
}
