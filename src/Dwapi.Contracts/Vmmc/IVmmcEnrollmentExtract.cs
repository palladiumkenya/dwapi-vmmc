using System;

namespace Dwapi.Vmmc.Contracts.Vmmc
{
    public interface IVmmcEnrollmentExtract
    {
        string FacilityName { get; set; }
        string Pkv { get; set; }
        string RecordUUID { get; set; }
        string VMMCId { get; set; }
        string VMMCIdHash { get; set; }
        DateTime? EncounterDate { get; set; }
        string ReferredBy { get; set; }
        string SourceVMMCInformation { get; set; }
        string CountyOfOrigin { get; set; }
        bool? Voided { get; set; }
    }
}
