using System.Collections.Generic;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class MasterFacilityRepository:BaseRepository<MasterFacility,int>, IMasterFacilityRepository
    {
        public MasterFacilityRepository(VmmcContext context) : base(context)
        {
        }

        public MasterFacility GetBySiteCode(int siteCode)
        {
            return DbSet.Find(siteCode);
        }

        public List<MasterFacility> GetLastSnapshots(int siteCode)
        {
            return DbSet.Where(x =>  x.SnapshotSiteCode == siteCode)
                .ToList();
        }
    }
}
