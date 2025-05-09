using System;
using Dwapi.Vmmc.Contracts.Vmmc;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class VmmcEnrollmentExtract : Entity<Guid>,IExtract,IVmmcEnrollmentExtract
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
        public string FacilityName { get; set; }
        public string Pkv { get; set; }
        
        public string RecordUUID { get; set; }
        public string VMMCId { get; set; }
        public string VMMCIdHash { get; set; }
        public DateTime? EncounterDate { get; set; }
        public string ReferredBy { get; set; }
        public string SourceVMMCInformation { get; set; }
        public string CountyOfOrigin { get; set; }
        public bool? Voided { get; set; }


        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }

    }
}
