using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Exchange;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Model;
using Serilog;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class FacilityRepository : BaseRepository<Facility, Guid>, IFacilityRepository
    {
        public FacilityRepository(VmmcContext context) : base(context)
        {
        }

        public IEnumerable<SiteProfile> GetSiteProfiles()
        {
            return GetAll().Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

        public IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes)
        {
            return GetAll(x=>siteCodes.Contains(x.SiteCode)).Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

        public IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds)
        {
            var list = new List<StatsDto>();
            foreach (var facilityId in facilityIds)
            {
                try
                {
                    var stat = GetFacStats(facilityId);
                    if(null!=stat)
                        list.Add(stat);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }


            }
            return list;
        }

        public StatsDto GetFacStats(Guid facilityId)
        {
            string sql = $@"
select
(select top 1 {nameof(Facility.SiteCode)} from {nameof(VmmcContext.Facilities)} where {nameof(Facility.Id)}='{facilityId}') FacilityCode,
(select ISNULL(max({nameof(VmmcEnrollmentExtract.Created)}),GETDATE()) from {nameof(VmmcContext.VmmcEnrollmentExtracts)} where {nameof(VmmcEnrollmentExtract.FacilityId)}='{facilityId}') Updated,
(select count(id) from {nameof(VmmcContext.VmmcEnrollmentExtracts)} where facilityid='{facilityId}') {nameof(VmmcEnrollmentExtract)},
(select count(id) from {nameof(VmmcContext.VmmcProcedureExtracts)} where facilityid='{facilityId}') {nameof(VmmcProcedureExtract)},
(select count(id) from {nameof(VmmcContext.VmmcFollowupVisitExtracts)} where facilityid='{facilityId}') {nameof(VmmcFollowupVisitExtract)},
(select count(id) from {nameof(VmmcContext.VmmcMhpeExtracts)} where facilityid='{facilityId}') {nameof(VmmcMhpeExtract)},
(select count(id) from {nameof(VmmcContext.VmmcDiscontinuationExtracts)} where facilityid='{facilityId}') {nameof(VmmcDiscontinuationExtract)},

";

            var result = GetDbConnection().Query<dynamic>(sql).FirstOrDefault();

            if (null != result)
            {
                var stats=new StatsDto(result.FacilityCode,result.Updated);
                stats.AddStats($"{nameof(VmmcEnrollmentExtract)}",result.VmmcEnrollmentExtract);
                stats.AddStats($"{nameof(VmmcProcedureExtract)}",result.VmmcProcedureExtract);
                stats.AddStats($"{nameof(VmmcFollowupVisitExtract)}",result.VmmcFollowupVisitExtract);
                stats.AddStats($"{nameof(VmmcMhpeExtract)}",result.VmmcMhpeExtract);
                stats.AddStats($"{nameof(VmmcDiscontinuationExtract)}",result.VmmcDiscontinuationExtract);

                return stats;
            }

            return null;
        }

        public Facility GetBySiteCode(int siteCode)
        {
            return DbSet.FirstOrDefault(x=>x.SiteCode==siteCode);
        }
    }
}
