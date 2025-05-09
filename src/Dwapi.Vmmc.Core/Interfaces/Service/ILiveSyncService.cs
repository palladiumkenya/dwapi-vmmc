using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Domain.Dto;

namespace Dwapi.Vmmc.Core.Interfaces.Service
{
    public interface ILiveSyncService
    {
        void SyncManifest(Manifest manifest,int clientCount);
        void SyncStats(List<Guid> facilityId);
       void SyncMetrics(List<MetricDto> metrics);
       Task SyncHandshake(List<HandshakeDto> handshakeDtos);
    }
}
