using System;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Domain.Dto;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Core.Interfaces.Service;
using Dwapi.Vmmc.SharedKernel.Enums;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.Vmmc.Core.Service
{
    public class ManifestService:IManifestService
    {
        private readonly IManifestRepository _manifestRepository;
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private  readonly ILiveSyncService _liveSyncService;

        public ManifestService(IManifestRepository manifestRepository, ILiveSyncService liveSyncService, IMasterFacilityRepository masterFacilityRepository)
        {
            _manifestRepository = manifestRepository;
            _liveSyncService = liveSyncService;
            _masterFacilityRepository = masterFacilityRepository;
        }

        public void Process(int siteCode)
        {
            var manifests = _manifestRepository.GetStaged(siteCode).ToList();
            if (manifests.Any())
            {
                var communityManifests = manifests.Where(x => x.EmrSetup == EmrSetup.Community).ToList();

                var otherManifests = manifests.Where(x => x.EmrSetup != EmrSetup.Community).ToList();

                try
                {
                    if (otherManifests.Any())
                        _manifestRepository.ClearFacility(otherManifests);
                }
                catch (Exception e)
                {
                    Log.Error( "Clear MANIFEST ERROR "+ e);
                }

                try
                {
                    if (communityManifests.Any())
                        _manifestRepository.ClearFacility(communityManifests, "IRDO");
                }
                catch (Exception e)
                {
                    Log.Error(e,"Clear Facility Data Error ");
                }

                foreach (var manifest in manifests)
                {
                    var clientCount = _manifestRepository.GetPatientCount(manifest.Id);
                    _manifestRepository.updateCount(manifest.Id,clientCount);
                    _liveSyncService.SyncManifest(manifest,clientCount);

                    try
                    {
                        // Get MasterFacility
                        var masterFacility = _masterFacilityRepository.GetBySiteCode(manifest.SiteCode);

                        if (null != masterFacility)
                        {
                            // Sync Metrics
                            var metricDtos = MetricDto.Generate(masterFacility, manifest);
                            _liveSyncService.SyncMetrics(metricDtos);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }

                }
            }
        }
    }
}
