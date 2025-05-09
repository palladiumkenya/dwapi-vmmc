using System;
using Dwapi.Vmmc.Contracts.Vmmc;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class VmmcProcedureExtract : Entity<Guid>,IExtract, IVmmcProcedureExtract
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
        
       
        
        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }

        public string RecordUUID { get; set; }
        public string VMMCId { get; set; }
        public string VMMCIdHash { get; set; }
        public string Method { get; set; }
        public string SurgicalMethod { get; set; }
        public string DeviceName { get; set; }
        public string DeviceSize { get; set; }
        public string AnaesthesiaUsed { get; set; }
        public string Agent { get; set; }
        public string Concentration { get; set; }
        public string Volume { get; set; }
        public string TimePlacementDevice { get; set; }
        public string TimeMakingLastSllit { get; set; }
        public string AdverseEvent { get; set; }
        public string AdverseEventtype { get; set; }
        public string AEDescription { get; set; }
        public string AESeverity { get; set; }
        public string dverseEventsManagement { get; set; }
        public string CadreClinician { get; set; }
        public string CadreAssistanClinician { get; set; }
        public string TheatreRegisterNumber { get; set; }
        public string BP { get; set; }
        public string PulseRate { get; set; }
        public string Temperature { get; set; }
        public string PenisElevatedAbdomen { get; set; }
        public string PostProcedureInstruction { get; set; }
        public string PostOperationMedicationGiven { get; set; }
        public string Drug { get; set; }
        public DateTime? RemovalDate { get; set; }
        public string ScheduledNextVisit { get; set; }
        public string CadreDischargedBy { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public bool? Voided { get; set; }
    }
}
