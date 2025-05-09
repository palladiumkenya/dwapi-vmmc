﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.SharedKernel.Utils;
using MediatR;

namespace Dwapi.Vmmc.Core.Command
{
    public class EnrollFacility : IRequest<Guid>
    {
        public int SiteCode { get; }
        public string Name { get; }
        public string Emr { get; set; }
        public bool AllowSnapshot { get; set; }

        public EnrollFacility(int siteCode, string name, string emr)
        {
            SiteCode = siteCode;
            Name = name;
            Emr = emr;
        }
    }
    public class EnrollFacilityHandler: IRequestHandler<EnrollFacility,Guid>
    {
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public EnrollFacilityHandler(IMasterFacilityRepository masterFacilityRepository, IFacilityRepository facilityRepository, IMediator mediator)
        {
            _masterFacilityRepository = masterFacilityRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(EnrollFacility request, CancellationToken cancellationToken)
        {
            var mfl =await  _mediator.Send(new ValidateFacility(request.SiteCode), cancellationToken);

            var facility =await _facilityRepository.GetAsync(x => x.SiteCode == request.SiteCode);

            request.Emr = request.Emr.IsSameAs("CHAK") ? "IQCare" : request.Emr;

            // Enroll New Site

            if (null == facility)
            {
                var newFacility = new Facility(request.SiteCode, request.Name, mfl.Id) {Emr = request.Emr};
                _facilityRepository.Create(newFacility);
                await _facilityRepository.SaveAsync();
                return newFacility.Id;
            }

            // Take Facility SnapShot

            if (facility.EmrChanged(request.Emr) && request.AllowSnapshot)
            {
                await _mediator.Send(new SnapMasterFacility(facility.SiteCode), cancellationToken);

                var newFacility = new Facility(request.SiteCode, request.Name, request.SiteCode) {Emr = request.Emr};

                _facilityRepository.Create(newFacility);
                await _facilityRepository.SaveAsync();
                return newFacility.Id;
            }



            return facility.Id;
        }
    }
}
