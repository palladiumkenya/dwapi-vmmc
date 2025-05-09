using System;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.Vmmc.Core.Command;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Core.Interfaces.Service;
using Dwapi.Vmmc.Core.Service;
using Dwapi.Vmmc.Filters;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.Infrastructure.Data.Repository;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Vmmc
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public static bool AllowSnapshot { get; set; }

        public Startup(IWebHostEnvironment environment,IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DwapiConnection"];
            var liveSync= Configuration["LiveSync"];
            var allowSnapshot= Configuration["AllowSnapshot"];

            services.AddControllersWithViews();

            try
            {
                services.AddDbContext<VmmcContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(VmmcContext).GetTypeInfo().Assembly.GetName().Name)));
                services.AddHangfire(o => o.UseSqlServerStorage(connectionString));
            }
            catch (Exception e)
            {
                Log.Error(e,"Startup error");
            }

            services.AddMediatR(typeof(SaveManifest).Assembly);
            services.AddScoped<IDocketRepository, DocketRepository>();
            services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();

            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IManifestRepository, ManifestRepository>();

            services.AddScoped<IVmmcEnrollmentExtractRepository, VmmcEnrollmentExtractRepository>();
            services.AddScoped<IVmmcProcedureExtractRepository, VmmcProcedureExtractRepository>();
            services.AddScoped<IVmmcFollowupVisitExtractRepository, VmmcFollowupVisitExtractRepository>();
            services.AddScoped<IVmmcMhpeExtractRepository, VmmcMhpeExtractRepository>();
            services.AddScoped<IVmmcDiscontinuationExtractRepository, VmmcDiscontinuationExtractRepository>();
 


            services.AddScoped<IManifestService, ManifestService>();
            services.AddScoped<IVmmcService, VmmcService>();
            services.AddScoped<ILiveSyncService, LiveSyncService>();
            if (!string.IsNullOrWhiteSpace(liveSync))
            {
                Uri endPointA = new Uri(liveSync); // this is the endpoint HttpClient will hit
                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = endPointA,
                };
                services.AddSingleton<HttpClient>(httpClient);
            }
            if (!string.IsNullOrWhiteSpace(allowSnapshot))
                AllowSnapshot = Convert.ToBoolean(allowSnapshot);
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DWAPI Central VMMC API");
                //c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            EnsureMigrationOfContext<VmmcContext>(serviceProvider);
            Mapper.Initialize(cfg =>
                {
                    cfg.AddDataReaderMapping();
                }
            );



            #region HangFire
            try
            {
                app.UseHangfireDashboard();

                var options = new BackgroundJobServerOptions {ServerName  = "DWAPIVMMCMAIN",WorkerCount = 1 };
                app.UseHangfireServer(options);
                GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
                GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Hangfire is down !");
            }
            #endregion

            try
            {
                DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
                if (!DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

            Log.Debug(@"initializing Database [Complete]");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug(@"

                        ________                        .__    _________                __                .__
                        \______ \__  _  _______  ______ |__|   \_   ___ \  ____   _____/  |_____________  |  |
                         |    |  \ \/ \/ /\__  \ \____ \|  |   /    \  \/_/ __ \ /    \   __\_  __ \__  \ |  |
                         |    `   \     /  / __ \|  |_> >  |   \     \___\  ___/|   |  \  |  |  | \// __ \|  |__
                        /_______  /\/\_/  (____  /   __/|__| /\ \______  /\___  >___|  /__|  |__|  (____  /____/
                                \/             \/|__|        \/        \/     \/     \/                 \/

            ");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug("Dwapi Central VMMC started !");
        }

        public static void EnsureMigrationOfContext<T>(IServiceProvider app) where T : BaseContext
        {
            var contextName = typeof(T).Name;
            Log.Debug($"initializing Database context: {contextName}");
            var context = app.GetService<T>();
            try
            {
                context.Database.Migrate();
                context.EnsureSeeded();
                context.Database.SetCommandTimeout(300);

                Log.Debug($"initializing Database context: {contextName} [OK]");
            }
            catch (Exception e)
            {
                Log.Debug($"initializing Database context: {contextName} Error");
                Log.Debug($"{e}");
            }
        }
    }
}
