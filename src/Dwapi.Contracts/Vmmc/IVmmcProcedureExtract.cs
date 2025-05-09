using System;

namespace Dwapi.Vmmc.Contracts.Vmmc
{
    public interface IVmmcProcedureExtract
    {
        string RecordUUID  { get; set; }
        string VMMCId  { get; set; }
        string VMMCIdHash  { get; set; }
        string Method  { get; set; }
        string SurgicalMethod  { get; set; }
        string DeviceName  { get; set; }
        string DeviceSize  { get; set; }
        string AnaesthesiaUsed  { get; set; }
        string Agent  { get; set; }
        string Concentration  { get; set; }
        string Volume  { get; set; }
        string TimePlacementDevice  { get; set; }
        string TimeMakingLastSllit  { get; set; }
        string AdverseEvent  { get; set; }
        string AdverseEventtype  { get; set; }
        string AEDescription  { get; set; }
        string AESeverity  { get; set; }
        string dverseEventsManagement  { get; set; }
        string CadreClinician  { get; set; }
        string CadreAssistanClinician  { get; set; }
        string TheatreRegisterNumber  { get; set; }
        string BP  { get; set; }
        string PulseRate  { get; set; }
        string Temperature  { get; set; }
        string PenisElevatedAbdomen  { get; set; }
        string PostProcedureInstruction  { get; set; }
        string PostOperationMedicationGiven  { get; set; }
        string Drug  { get; set; }
        DateTime? RemovalDate  { get; set; }
        string ScheduledNextVisit  { get; set; }
        string CadreDischargedBy  { get; set; }
        DateTime? Date_Created  { get; set; }
        DateTime? Date_Last_Modified  { get; set; }
        bool? Voided  { get; set; }
    }
}
