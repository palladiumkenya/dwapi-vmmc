using System.Threading;
using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using MediatR;

namespace Dwapi.Vmmc.Core.Command
{
    public class SnapMasterFacility : IRequest<bool>
    {
        public int SiteCode { get; }

        public SnapMasterFacility(int siteCode)
        {
            SiteCode = siteCode;
        }
    }
    public class SnapMasterFacilityHandler: IRequestHandler<SnapMasterFacility,bool>
    {
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SnapMasterFacilityHandler(IMasterFacilityRepository masterFacilityRepository, IFacilityRepository facilityRepository, IMediator mediator)
        {
            _masterFacilityRepository = masterFacilityRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(SnapMasterFacility request, CancellationToken cancellationToken)
        {
            var mfl = _masterFacilityRepository.GetBySiteCode(request.SiteCode);
            var mflSnaps = _masterFacilityRepository.GetLastSnapshots(request.SiteCode);

            if (null == mfl)
                return true;

            var snapMfl=mfl.TakeSnap(mflSnaps);
            _masterFacilityRepository.Create(snapMfl);
            _masterFacilityRepository.Save();

            var fl = _facilityRepository.GetBySiteCode(request.SiteCode);

            if (null == fl)
                return true;

            var snapfl=fl.TakeSnapFrom(snapMfl);
            _facilityRepository.Save();

            return await Task.FromResult(true);
        }
    }
}
