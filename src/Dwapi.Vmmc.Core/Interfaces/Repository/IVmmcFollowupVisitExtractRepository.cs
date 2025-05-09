using System;
using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IVmmcFollowupVisitExtractRepository : IRepository<VmmcFollowupVisitExtract,Guid>
    {
        void Process(Guid facilityId,IEnumerable<VmmcFollowupVisitExtract> clients);
    }
}
