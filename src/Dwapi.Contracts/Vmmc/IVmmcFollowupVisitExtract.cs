using System;

namespace Dwapi.Vmmc.Contracts.Vmmc
{
    public interface IVmmcFollowupVisitExtract
    {
        string? FacilityName { get; set; }
        string? RecordUUID  { get; set; }
        string? VMMCId  { get; set; }
        
        DateTime? EncounterDate  { get; set; }
        string? VisitType  { get; set; }
        string? DaySinceLastCircumsicion  { get; set; }
        string? AdverseEventPostCircumcision  { get; set; }
        string? AEType  { get; set; }
        string? AEDescription  { get; set; }
        string? AESeverity  { get; set; }
        string? AEManagement  { get; set; }
        string? MedicationGiven  { get; set; }
        string? Drug  { get; set; }
        string? CadreClinician  { get; set; }
        DateTime? Date_Created  { get; set; }
        DateTime? Date_Last_Modified  { get; set; }
        bool? Voided  { get; set; }
    }
}
