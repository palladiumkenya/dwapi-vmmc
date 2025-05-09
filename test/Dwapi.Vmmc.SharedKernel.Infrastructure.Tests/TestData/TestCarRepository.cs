using System;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Tests.TestData.TestData.Interfaces;
using Dwapi.Vmmc.SharedKernel.Tests.TestData.TestData.Models;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.Vmmc.SharedKernel.Infrastructure.Tests.TestData
{

    public class TestCarRepository :BaseRepository<TestCar,Guid>,  ITestCarRepository
    {
        public TestCarRepository(DbContext context) : base(context)
        {
        }
    }
}
