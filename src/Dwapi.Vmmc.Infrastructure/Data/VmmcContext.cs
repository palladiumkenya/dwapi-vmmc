using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Vmmc.Infrastructure.Data
{
    public class VmmcContext : BaseContext
    {
        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }

        public DbSet<VmmcEnrollmentExtract> VmmcEnrollmentExtracts { get; set; }
        public DbSet<VmmcProcedureExtract> VmmcProcedureExtracts { get; set; }
        public DbSet<VmmcFollowupVisitExtract> VmmcFollowupVisitExtracts { get; set; }
        public DbSet<VmmcMhpeExtract> VmmcMhpeExtracts { get; set; }
        public DbSet<VmmcDiscontinuationExtract> VmmcDiscontinuationExtracts { get; set; }


        public VmmcContext(DbContextOptions<VmmcContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(VmmcContext.Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(VmmcContext.Subscribers)}");
            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Id).Table($"{nameof(VmmcContext.MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Id).Table($"{nameof(VmmcContext.Facilities)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(VmmcContext.Manifests)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(VmmcContext.Cargoes)}");

            DapperPlusManager.Entity<VmmcEnrollmentExtract>().Key(x => x.Id).Table($"{nameof(VmmcContext.VmmcEnrollmentExtracts)}");
            DapperPlusManager.Entity<VmmcProcedureExtract>().Key(x => x.Id).Table($"{nameof(VmmcContext.VmmcProcedureExtracts)}");
            DapperPlusManager.Entity<VmmcFollowupVisitExtract>().Key(x => x.Id).Table($"{nameof(VmmcContext.VmmcFollowupVisitExtracts)}");
            DapperPlusManager.Entity<VmmcMhpeExtract>().Key(x => x.Id).Table($"{nameof(VmmcContext.VmmcMhpeExtracts)}");
            DapperPlusManager.Entity<VmmcDiscontinuationExtract>().Key(x => x.Id).Table($"{nameof(VmmcContext.VmmcDiscontinuationExtracts)}");

        }

        public override void EnsureSeeded()
        {
            Log.Debug("seeding...");
            if (!MasterFacilities.Any())
            {
                var data = SeedDataReader.ReadCsv<MasterFacility>(typeof(VmmcContext).Assembly,"Seed","|");
                MasterFacilities.AddRange(data);
            }

            if (!Dockets.Any())
            {
                var data = SeedDataReader.ReadCsv<Docket>(typeof(VmmcContext).Assembly,"Seed","|");
                Dockets.AddRange(data);
            }

            if (!Subscribers.Any())
            {
                var data = SeedDataReader.ReadCsv<Subscriber>(typeof(VmmcContext).Assembly,"Seed","|");
                Subscribers.AddRange(data);
            }
            SaveChanges();
            Log.Debug("seeding DONE");
        }
    }
}
