using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Command;
using Dwapi.Vmmc.Core.Domain.Dto;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Core.Interfaces.Service;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;

    
namespace Dwapi.Vmmc.Controllers
{
    [Route("api/[controller]")]
    public class VmmcController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestService _manifestService;
        private readonly IVmmcService _VmmcService;

        public VmmcController(IMediator mediator, IManifestRepository manifestRepository,
            IManifestService manifestService, IVmmcService VmmcService)
        {
            _mediator = mediator;
            _manifestService = manifestService;
            _VmmcService = VmmcService;
        }

        // POST api/Vmmc/verify
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody] SubscriberDto subscriber)
        {
            if (null == subscriber)
                return BadRequest();

            try
            {
                var dockect = await _mediator.Send(new VerifySubscriber(subscriber.SubscriberId,subscriber.AuthToken), HttpContext.RequestAborted);
                return Ok(dockect);
            }
            catch (Exception e)
            {
                Log.Error(e, "verify error");
                return StatusCode(500, e.Message);
            }
        }

        // POST api/Vmmc/Manifest
        [HttpPost("Manifest")]
        public async Task<IActionResult> ProcessManifest([FromBody] ManifestExtractDto manifestDto)
        {
            if (null == manifestDto)
                return BadRequest();
            
            // check if version allowed to send
            // var version = manifestDto.Manifest.Cargoes.Select(x =>  x).Where(m => m.Items.Contains("VmmcService")).FirstOrDefault().Items;
            // var DwapiVersionSending = Int32.Parse((JObject.Parse(version)["Version"].ToString()).Replace(".", string.Empty));
            //
            // var config = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .Build();
            // var DwapiVersionCuttoff = Int32.Parse(config["DwapiVersionCuttoff"]);
            //
            // var currentLatestVersion = config["currentLatestVersion"];
            //
            // if (DwapiVersionSending < DwapiVersionCuttoff)
            // {
            //     return StatusCode(500, $" ====> You're using DWAPI Version [{DwapiVersionSending}]. Older Versions of DWAPI are " +
            //                            $"not allowed to send to NDWH. UPGRADE to the latest version {currentLatestVersion} and RELOAD and SEND");
            // }

            try
            {
                var manifest = new SaveManifest(manifestDto.Manifest);
                manifest.AllowSnapshot = Startup.AllowSnapshot;
                var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _manifestService.Process(manifest.Manifest.SiteCode));
                return Ok(new
                {
                    FacilityKey = faciliyKey
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("VmmcEnrollments")]
        public IActionResult ProcessVmmcEnrollments([FromBody] VmmcExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _VmmcService.Process(extract.VmmcEnrollmentExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "VmmcEnrollments error");
                return StatusCode(500, e.Message);
            }
        }

      
        [HttpPost("VmmcProcedures")]
        public IActionResult ProcessVmmcProcedures([FromBody] VmmcExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _VmmcService.Process(extract.VmmcProcedureExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "VmmcProcedures error");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("VmmcFollowupVisits")]
        public IActionResult ProcessVmmcFollowupVisits([FromBody] VmmcExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _VmmcService.Process(extract.VmmcFollowupVisitExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "VmmcFollowupVisits error");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("VmmcMpeExtracts")]
        public IActionResult ProcessVmmcMpeExtracts([FromBody] VmmcExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _VmmcService.Process(extract.VmmcMhpeExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "VmmcMpeExtracts error");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("VmmcDiscontinuation")]
        public IActionResult ProcessVmmcDiscontinuation([FromBody] VmmcExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _VmmcService.Process(extract.VmmcDiscontinuationExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "VmmcDiscontinuation error");
                return StatusCode(500, e.Message);
            }
        }
        

        // POST api/Vmmc/Status
        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            try
            {
                var ver = GetType().Assembly.GetName().Version;
                return Ok(new
                {
                    name = "Dwapi Central - API (Vmmc)",
                    status = "running",
                    version = "v1.0.0.1",
                    build = "05JUL221246"
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "status error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
