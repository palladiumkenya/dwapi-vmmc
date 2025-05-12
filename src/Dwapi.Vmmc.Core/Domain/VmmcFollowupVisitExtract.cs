using System;
using Dwapi.Vmmc.Contracts.Vmmc;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class VmmcFollowupVisitExtract : Entity<Guid>,IExtract, IVmmcFollowupVisitExtract
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool? Processed { get; set; }
        public string? QueueId { get; set; }
        public string? Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public Guid FacilityId { get; set; }
        

        
        
        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
        public string? FacilityName { get; set; }
        public string? RecordUUID { get; set; }
        public string? VMMCId { get; set; }
        public DateTime? EncounterDate { get; set; }
        public string? VisitType { get; set; }
        public string? DaySinceLastCircumsicion { get; set; }
        public string? AdverseEventPostCircumcision { get; set; }
        public string? AEType { get; set; }
        public string? AEDescription { get; set; }
        public string? AESeverity { get; set; }
        public string? AEManagement { get; set; }
        public string? MedicationGiven { get; set; }
        public string? Drug { get; set; }
        public string? CadreClinician { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public bool? Voided { get; set; }
    }
}
