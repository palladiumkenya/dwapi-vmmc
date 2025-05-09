using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class VmmcFollowupVisitExtractRepository : BaseRepository<VmmcFollowupVisitExtract,Guid>, IVmmcFollowupVisitExtractRepository{public VmmcFollowupVisitExtractRepository(VmmcContext context) : base(context){}public void Process(Guid facilityId,IEnumerable<VmmcFollowupVisitExtract> extracts){var mpi = extracts.ToList();if (mpi.Any()){mpi.ForEach(x => x.FacilityId = facilityId);CreateBulk(mpi);}}}
}