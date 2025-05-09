using System;
using System.Collections.Generic;

namespace Dwapi.Vmmc.Core.Exchange
{
    public class StatsDto
    {
        public int FacilityCode { get; set; }
        public DocketDto Docket { get; set; }
        public List<StatDto> Stats { get; set; }=new List<StatDto>();
        public DateTime Updated { get; set; }

        public StatsDto(int facilityCode, DateTime updated)
        {
            FacilityCode = facilityCode;
            Docket = new DocketDto("Vmmc");
            Updated = updated;
        }

        public void AddStats(string extract,int count)
        {
            Stats.Add(new StatDto(extract,count));
        }
    }
}
