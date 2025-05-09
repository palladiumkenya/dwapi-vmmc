using System;
using Dwapi.Vmmc.SharedKernel.Interfaces;
using Dwapi.Vmmc.SharedKernel.Tests.TestData.TestData.Models;

namespace Dwapi.Vmmc.SharedKernel.Tests.TestData.TestData.Interfaces
{
    public interface ITestCarRepository : IRepository<TestCar,Guid>
    {

    }
}
