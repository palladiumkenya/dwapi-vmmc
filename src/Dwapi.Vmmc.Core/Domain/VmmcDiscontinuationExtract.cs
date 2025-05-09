using System;
using Dwapi.Vmmc.Contracts.Vmmc;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class VmmcDiscontinuationExtract : Entity<Guid>,IExtract, IVmmcDiscontinuationExtract
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool? Processed { get; set; }
        public string QueueId { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public Guid FacilityId { get; set; }
        
        public string RecordUUID { get; set; }
        public string VMMCId { get; set; }
        public string VMMCIdHash { get; set; }
        public DateTime? DiscontinuationDate { get; set; }
        public string Reason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public bool? Voided { get; set; }
        
        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
