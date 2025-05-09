﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.Infrastructure.Data.Repository;
using Dwapi.Vmmc.SharedKernel.Tests.TestData.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Vmmc.Infrastructure.Tests.Data.Repository
{
    [TestFixture]
    [NUnit.Framework.Category("UsesDb")]
    public class ManifestRepositoryTests
    {
        private ServiceProvider _serviceProvider;
        private VmmcContext _context;
        private List<Facility> _facilities;
        private IManifestRepository _manifestRepository;
        private List<Manifest> _manifests;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<VmmcContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IManifestRepository, ManifestRepository>()
                .BuildServiceProvider();

            _facilities = TestDataFactory.TestFacilityWithPatients(2);
            _manifests = TestDataFactory.TestManifests(2);

            _manifests[0].FacilityId = _facilities[0].Id;
            _manifests[1].FacilityId = _facilities[1].Id;

            _context = _serviceProvider.GetService<VmmcContext>();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.MasterFacilities.AddRange(TestDataFactory.TestMasterFacilities());
            _context.Facilities.AddRange(_facilities);
            _context.Manifests.AddRange(_manifests);
            _context.SaveChanges();
        }

        [SetUp]
        public void Setup()
        {
            _manifestRepository = _serviceProvider.GetService<IManifestRepository>();
        }

        [Test]
        public void should_Clear_With_Manifest_Facility()
        {
            var patients = _context.VmmcEnrollmentExtracts;
            Assert.True(patients.Any());
           _manifestRepository.ClearFacility(_manifests);
            var nopatients = _context.VmmcEnrollmentExtracts;
            Assert.False(nopatients.Any());
        }

        [Test]
        public void should_Get_Count()
        {
            var patients = _manifestRepository.GetPatientCount(_manifests.First().Id);
            Assert.True(patients>0);;
        }
    }
}
