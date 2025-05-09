using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Domain.Dto;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IManifestRepository : IRepository<Manifest, Guid>
    {
        void ClearFacility(IEnumerable<Manifest> manifests);
        void ClearFacility(IEnumerable<Manifest> manifests,string project);
        int GetPatientCount(Guid id);
        IEnumerable<Manifest> GetStaged(int siteCode);
        Task EndSession(Guid session);
        IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session);
        void updateCount(Guid id,int clientCount);
        string GetDWAPIversionSending(int siteCode);

    }
}
