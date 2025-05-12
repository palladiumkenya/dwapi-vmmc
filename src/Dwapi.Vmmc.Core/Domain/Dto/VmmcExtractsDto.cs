using System.Collections.Generic;
using Dwapi.Vmmc.Core.Domain;
    
namespace Dwapi.Vmmc.Core.Domain.Dto
{
    public class VmmcExtractsDto
    {
        // public List<VmmcEnrollmentExtract> VmmcEnrollmentExtracts { get; set; } = new List<VmmcEnrollmentExtract>();
        // public List<VmmcProcedureExtract> VmmcProcedureExtracts { get; set; } = new List<VmmcProcedureExtract>();
        // public List<VmmcFollowupVisitExtract> VmmcFollowupVisitExtracts { get; set; } = new List<VmmcFollowupVisitExtract>();
        // public List<VmmcMhpeExtract> VmmcMhpeExtracts { get; set; } = new List<VmmcMhpeExtract>();
        // public List<VmmcDiscontinuationExtract> VmmcDiscontinuationExtracts { get; set; } = new List<VmmcDiscontinuationExtract>();

        public List<VmmcEnrollmentExtract> Enrollments { get; set; }=new List<VmmcEnrollmentExtract>();
        public List<VmmcDiscontinuationExtract> Discontinuations { get; set; }=new List<VmmcDiscontinuationExtract>();
        public List<VmmcProcedureExtract> Procedures { get; set; }=new List<VmmcProcedureExtract>();
        public List<VmmcFollowupVisitExtract> FollowupVisits { get; set; } = new List<VmmcFollowupVisitExtract>();
        public List<VmmcMhpeExtract> MhpeExtracts { get; set; } = new List<VmmcMhpeExtract>();
    }
}
