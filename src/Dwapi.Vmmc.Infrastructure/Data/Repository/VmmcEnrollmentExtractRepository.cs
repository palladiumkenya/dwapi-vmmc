using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class VmmcEnrollmentExtractRepository : BaseRepository<VmmcEnrollmentExtract,Guid>, IVmmcEnrollmentExtractRepository
    {
        public VmmcEnrollmentExtractRepository(VmmcContext context) : base(context)
        {
        }

        public void Process(Guid facilityId,IEnumerable<VmmcEnrollmentExtract> clients)
        {
            var mpi = clients.ToList();

            if (mpi.Any())
            {
                mpi.ForEach(x => x.FacilityId = facilityId);
                CreateBulk(mpi);
            }
        }
    }
}