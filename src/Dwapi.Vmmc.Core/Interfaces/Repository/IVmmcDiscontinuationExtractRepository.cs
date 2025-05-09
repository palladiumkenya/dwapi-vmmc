using System;
using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IVmmcDiscontinuationExtractRepository : IRepository<VmmcDiscontinuationExtract,Guid>
    {
        void Process(Guid facilityId,IEnumerable<VmmcDiscontinuationExtract> clients);
    }
}
