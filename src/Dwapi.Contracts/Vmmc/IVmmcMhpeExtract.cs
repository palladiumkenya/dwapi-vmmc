using System;

namespace Dwapi.Vmmc.Contracts.Vmmc
{
    public interface IVmmcMhpeExtract
    {
        string? FacilityName { get; set; }
        string? RecordUUID  { get; set; }
        string? VMMCId  { get; set; }
        
        DateTime?  EncounterDate  { get; set; }
        string? HIVStatus  { get; set; }
        string? Reasonresultsunknown  { get; set; }
        string? ReferredServices  { get; set; }
        string? HIVStatusSelfReport  { get; set; }
        string? FacilityReceivingCare  { get; set; }
        string? CCCNumber  { get; set; }
        string? CurrentRegimen  { get; set; }
        string? LastVL  { get; set; }
        string? CD4Count  { get; set; }
        string? BleedingDisorder  { get; set; }
        string? Diabetes  { get; set; }
        string? UrethralDischarge  { get; set; }
        string? GenitalSore  { get; set; }
        string? PainUrination  { get; set; }
        string? SwellingScrotum  { get; set; }
        string? DifficultyRetractingForeskin  { get; set; }
        string? DifficultyReturninForeskinNormal  { get; set; }
        string? SexualFunctionConcerns  { get; set; }
        string? Epispadia  { get; set; }
        string? Hypospadia  { get; set; }
        string? Other  { get; set; }
        string? Anaemia  { get; set; }
        string? HIVAIDS  { get; set; }
        DateTime?  StartARTDate  { get; set; }
        string? Adherance  { get; set; }
        DateTime?  NextAppointmentDate  { get; set; }
        string? HB  { get; set; }
        string? SugarLevels  { get; set; }
        string? ClientEverHadSurgery  { get; set; }
        string? SpecifySurgery  { get; set; }
        string? TetanusBoosterGiven  { get; set; }
        DateTime?  DateTetanusBoosterGiven  { get; set; }
        string? BP  { get; set; }
        string? PulseRate  { get; set; }
        string? Temperature  { get; set; }
        DateTime? Date_Created  { get; set; }
        DateTime? Date_Last_Modified  { get; set; }
        bool? Voided  { get; set; }
    }
}
