using System;

namespace Dwapi.Vmmc.Contracts.Vmmc
{
    public interface IVmmcDiscontinuationExtract
    {
        string RecordUUID  { get; set; }
        string VMMCId  { get; set; }
        string VMMCIdHash  { get; set; }
        DateTime? DiscontinuationDate  { get; set; }
        string Reason  { get; set; }
        DateTime? Date_Created  { get; set; }
        DateTime? Date_Last_Modified  { get; set; }
        bool? Voided  { get; set; }
    }
}
