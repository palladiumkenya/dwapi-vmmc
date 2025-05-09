using System;
using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Exchange;
using Dwapi.Vmmc.SharedKernel.Interfaces;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IFacilityRepository : IRepository<Facility, Guid>
    {
        IEnumerable<SiteProfile> GetSiteProfiles();
        IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes);

        IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds);
        StatsDto GetFacStats(Guid facilityId);
        Facility GetBySiteCode(int siteCode);
    }
}
