using System;
using Dwapi.Vmmc.SharedKernel.Enums;
using Dwapi.Vmmc.SharedKernel.Model;

namespace Dwapi.Vmmc.Core.Domain
{
    public class Cargo : Entity<Guid>
    {
        public CargoType Type { get; set; }
        public string Items { get; set; }
        public Guid ManifestId { get; set; }

        public Cargo()
        {
        }
    }
}