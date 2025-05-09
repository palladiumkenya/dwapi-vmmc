using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.Vmmc.SharedKernel.Model;
using Dwapi.Vmmc.SharedKernel.Utils;

namespace Dwapi.Vmmc.Core.Domain
{
    public class Facility : Entity<Guid>
    {
        public int SiteCode { get; set; }
        [MaxLength(120)] public string Name { get; set; }
        public int? MasterFacilityId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Emr { get; set; }
        public DateTime? SnapshotDate { get; set; }
        public int? SnapshotSiteCode { get; set; }
        public int? SnapshotVersion { get; set; }

        public ICollection<VmmcEnrollmentExtract> VmmcEnrollmentExtracts { get; set; }=new List<VmmcEnrollmentExtract>();
        public virtual ICollection<VmmcProcedureExtract> VmmcProcedureExtracts { get; set; } = new List<VmmcProcedureExtract>();
        public virtual ICollection<VmmcFollowupVisitExtract> VmmcFollowupVisitExtracts { get; set; } = new List<VmmcFollowupVisitExtract>();
        public virtual ICollection<VmmcMhpeExtract> VmmcMhpeExtracts { get; set; } = new List<VmmcMhpeExtract>();
        public virtual ICollection<VmmcDiscontinuationExtract> VmmcDiscontinuationExtracts { get; set; } = new List<VmmcDiscontinuationExtract>();


        public ICollection<Manifest> Manifests { get; set; }=new List<Manifest>();

        public Facility()
        {
        }

        public Facility(int siteCode, string name)
        {
            SiteCode = siteCode;
            Name = name;
        }

        public Facility(int siteCode, string name, int? masterFacilityId):this(siteCode,name)
        {
            MasterFacilityId = masterFacilityId;
        }

        public bool EmrChanged(string requestEmr)
        {
            if (string.IsNullOrWhiteSpace(requestEmr))
                return false;

            if (string.IsNullOrWhiteSpace(Emr))
                return false;

            if (requestEmr.IsSameAs("CHAK"))
                requestEmr = "IQCare";

            if (requestEmr.IsSameAs("IQCare") || requestEmr.IsSameAs("KenyaEMR"))
                return !Emr.IsSameAs(requestEmr);

            return false;
        }

        public Facility TakeSnapFrom(MasterFacility snapMfl)
        {
            var fac = this;

            fac.SnapshotDate = DateTime.Now;
            fac.SiteCode = snapMfl.Id;
            fac.SnapshotSiteCode = snapMfl.SnapshotSiteCode;
            fac.SnapshotVersion = snapMfl.SnapshotVersion;

            return fac;
        }

        public override string ToString()
        {
            return $"{Name} - {SiteCode}";
        }
    }
}
