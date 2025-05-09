using System;
using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IVmmcMhpeExtractRepository : IRepository<VmmcMhpeExtract,Guid>
    {
        void Process(Guid facilityId,IEnumerable<VmmcMhpeExtract> clients);
    }
}
