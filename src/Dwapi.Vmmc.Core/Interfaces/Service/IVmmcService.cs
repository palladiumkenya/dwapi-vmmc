using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;

namespace Dwapi.Vmmc.Core.Interfaces.Service
{
    public interface IVmmcService
    {
        void Process(IEnumerable<VmmcEnrollmentExtract> patients);
        void Process(IEnumerable<VmmcProcedureExtract> extracts);
        void Process(IEnumerable<VmmcFollowupVisitExtract> extracts);
        void Process(IEnumerable<VmmcMhpeExtract> extracts);
        void Process(IEnumerable<VmmcDiscontinuationExtract> extracts);

    }
}
