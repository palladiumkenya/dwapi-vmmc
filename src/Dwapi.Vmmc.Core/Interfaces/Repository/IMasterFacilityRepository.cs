using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IMasterFacilityRepository:IRepository<MasterFacility,int>
    {
        MasterFacility GetBySiteCode(int siteCode);
        List<MasterFacility> GetLastSnapshots(int siteCode);
    }
}
