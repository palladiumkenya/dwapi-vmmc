using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
    
namespace Dwapi.Vmmc.Core.Domain.Dto
{
    public class VmmcExtractsDto
    {
        public List<VmmcEnrollmentExtract> VmmcEnrollmentExtracts { get; set; } = new List<VmmcEnrollmentExtract>();
        public List<VmmcProcedureExtract> VmmcProcedureExtracts { get; set; } = new List<VmmcProcedureExtract>();
        public List<VmmcFollowupVisitExtract> VmmcFollowupVisitExtracts { get; set; } = new List<VmmcFollowupVisitExtract>();
        public List<VmmcMhpeExtract> VmmcMhpeExtracts { get; set; } = new List<VmmcMhpeExtract>();
        public List<VmmcDiscontinuationExtract> VmmcDiscontinuationExtracts { get; set; } = new List<VmmcDiscontinuationExtract>();

    }
}
