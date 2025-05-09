using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Core.Interfaces.Service;
using Dwapi.Vmmc.SharedKernel.Exceptions;
using Dwapi.Vmmc.SharedKernel.Model;
using Serilog;

namespace Dwapi.Vmmc.Core.Service
{
    public class VmmcService : IVmmcService
    {

        private readonly IFacilityRepository _facilityRepository;

        private readonly ILiveSyncService _syncService;

        private List<SiteProfile> _siteProfiles = new List<SiteProfile>();
        
        private readonly IVmmcEnrollmentExtractRepository _vmmcEnrollmentExtractRepository;
        private readonly IVmmcProcedureExtractRepository _vmmcProcedureExtractRepository;
        private readonly IVmmcFollowupVisitExtractRepository _vmmcFollowupVisitExtractRepository;
        private readonly IVmmcMhpeExtractRepository _vmmcMhpeExtractRepository;
        private readonly IVmmcDiscontinuationExtractRepository _vmmcDiscontinuationExtractRepository;

        public VmmcService(IVmmcEnrollmentExtractRepository vmmcEnrollmentExtractRepository, IVmmcProcedureExtractRepository vmmcProcedureExtractRepository, IFacilityRepository facilityRepository, ILiveSyncService syncService, 
            IVmmcFollowupVisitExtractRepository vmmcFollowupVisitExtractRepository,IVmmcMhpeExtractRepository vmmcMhpeExtractRepository,IVmmcDiscontinuationExtractRepository vmmcDiscontinuationExtractRepository)
        {
            _facilityRepository = facilityRepository;
            _syncService = syncService;

            _vmmcEnrollmentExtractRepository = vmmcEnrollmentExtractRepository;
            _vmmcProcedureExtractRepository = vmmcProcedureExtractRepository;
            _vmmcFollowupVisitExtractRepository = vmmcFollowupVisitExtractRepository;
            _vmmcMhpeExtractRepository = vmmcMhpeExtractRepository;
            _vmmcDiscontinuationExtractRepository = vmmcDiscontinuationExtractRepository;
        }

        public void Process(IEnumerable<VmmcEnrollmentExtract> patients)
        {
            List<Guid> facilityIds=new List<Guid>();

            if(null==patients)
                return;
            if(!patients.Any())
                return;

            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();

            var batch = new List<VmmcEnrollmentExtract>();
            int count = 0;

            foreach (var patient in patients)
            {
                count++;
                try
                {
                    patient.FacilityId = GetFacilityId(patient.SiteCode);
                    patient.UpdateRefId();
                    batch.Add(patient);

                    facilityIds.Add(patient.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {patient.SiteCode}");
                }


                if (count == 1000)
                {
                    _vmmcEnrollmentExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<VmmcEnrollmentExtract>();
                }

            }

            if (batch.Any())
                _vmmcEnrollmentExtractRepository.CreateBulk(batch);

            SyncClients(facilityIds);

        }

        public void Process(IEnumerable<VmmcProcedureExtract> extracts)
        {
            List<Guid> facilityIds=new List<Guid>();
            if(null==extracts)
                return;
            if(!extracts.Any())
                return;
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();
            var batch = new List<VmmcProcedureExtract>();
            int count = 0;
            foreach (var extract in extracts)
            {
                count++;
                try
                {
                    extract.FacilityId = GetFacilityId(extract.SiteCode);
                    extract.UpdateRefId();
                    batch.Add(extract);
                    facilityIds.Add(extract.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {extract.SiteCode}");
                }


                if (count == 1000)
                {
                    _vmmcProcedureExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<VmmcProcedureExtract>();
                }
            }
            if (batch.Any())
                _vmmcProcedureExtractRepository.CreateBulk(batch);
            SyncClients(facilityIds);
        }
        

        public void Process(IEnumerable<VmmcFollowupVisitExtract> extracts)
        {
            List<Guid> facilityIds=new List<Guid>();
            if(null==extracts)
                return;
            if(!extracts.Any())
                return;
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();
            var batch = new List<VmmcFollowupVisitExtract>();
            int count = 0;
            foreach (var extract in extracts)
            {
                count++;
                try
                {
                    extract.FacilityId = GetFacilityId(extract.SiteCode);
                    extract.UpdateRefId();
                    batch.Add(extract);
                    facilityIds.Add(extract.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {extract.SiteCode}");
                }


                if (count == 1000)
                {
                    _vmmcFollowupVisitExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<VmmcFollowupVisitExtract>();
                }
            }
            if (batch.Any())
                _vmmcFollowupVisitExtractRepository.CreateBulk(batch);
            SyncClients(facilityIds);
        }

        public void Process(IEnumerable<VmmcMhpeExtract> extracts)
        {
            List<Guid> facilityIds=new List<Guid>();
            if(null==extracts)
                return;
            if(!extracts.Any())
                return;
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();
            var batch = new List<VmmcMhpeExtract>();
            int count = 0;
            foreach (var extract in extracts)
            {
                count++;
                try
                {
                    extract.FacilityId = GetFacilityId(extract.SiteCode);
                    extract.UpdateRefId();
                    batch.Add(extract);
                    facilityIds.Add(extract.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {extract.SiteCode}");
                }


                if (count == 1000)
                {
                    _vmmcMhpeExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<VmmcMhpeExtract>();
                }
            }
            if (batch.Any())
                _vmmcMhpeExtractRepository.CreateBulk(batch);
            SyncClients(facilityIds);
        }

        public void Process(IEnumerable<VmmcDiscontinuationExtract> extracts)
        {
            List<Guid> facilityIds=new List<Guid>();
            if(null==extracts)
                return;
            if(!extracts.Any())
                return;
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();
            var batch = new List<VmmcDiscontinuationExtract>();
            int count = 0;
            foreach (var extract in extracts)
            {
                count++;
                try
                {
                    extract.FacilityId = GetFacilityId(extract.SiteCode);
                    extract.UpdateRefId();
                    batch.Add(extract);
                    facilityIds.Add(extract.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {extract.SiteCode}");
                }


                if (count == 1000)
                {
                    _vmmcDiscontinuationExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<VmmcDiscontinuationExtract>();
                }
            }
            if (batch.Any())
                _vmmcDiscontinuationExtractRepository.CreateBulk(batch);
            SyncClients(facilityIds);
        }

        public Guid GetFacilityId(int siteCode)
        {
            var profile = _siteProfiles.FirstOrDefault(x => x.SiteCode == siteCode);
            if (null == profile)
                throw new FacilityNotFoundException(siteCode);

            return profile.FacilityId;
        }

        private void SyncClients(List<Guid> facIlds)
        {
            if (facIlds.Any())
            {
                _syncService.SyncStats(facIlds.Distinct().ToList());
            }
        }
    }
}
