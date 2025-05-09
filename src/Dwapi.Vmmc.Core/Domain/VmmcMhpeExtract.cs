using System;
using Dwapi.Vmmc.Contracts.Vmmc;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class VmmcMhpeExtract : Entity<Guid>,IExtract, IVmmcMhpeExtract
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
        public DateTime? EncounterDate { get; set; }
        public string HIVStatus { get; set; }
        public string Reasonresultsunknown { get; set; }
        public string ReferredServices { get; set; }
        public string HIVStatusSelfReport { get; set; }
        public string FacilityReceivingCare { get; set; }
        public string CCCNumber { get; set; }
        public string CurrentRegimen { get; set; }
        public string LastVL { get; set; }
        public string CD4Count { get; set; }
        public string BleedingDisorder { get; set; }
        public string Diabetes { get; set; }
        public string UrethralDischarge { get; set; }
        public string GenitalSore { get; set; }
        public string PainUrination { get; set; }
        public string SwellingScrotum { get; set; }
        public string DifficultyRetractingForeskin { get; set; }
        public string DifficultyReturninForeskinNormal { get; set; }
        public string SexualFunctionConcerns { get; set; }
        public string Epispadia { get; set; }
        public string Hypospadia { get; set; }
        public string Other { get; set; }
        public string Anaemia { get; set; }
        public string HIVAIDS { get; set; }
        public DateTime? StartARTDate { get; set; }
        public string Adherance { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string HB { get; set; }
        public string SugarLevels { get; set; }
        public string ClientEverHadSurgery { get; set; }
        public string SpecifySurgery { get; set; }
        public string TetanusBoosterGiven { get; set; }
        public DateTime? DateTetanusBoosterGiven { get; set; }
        public string BP { get; set; }
        public string PulseRate { get; set; }
        public string Temperature { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public bool? Voided { get; set; }
    }
}
