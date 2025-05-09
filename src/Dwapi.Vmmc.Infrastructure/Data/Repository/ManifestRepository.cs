using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Domain.Dto;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Enums;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class ManifestRepository : BaseRepository<Manifest, Guid>, IManifestRepository
    {
        public ManifestRepository(VmmcContext context) : base(context)
        {
        }

        public void ClearFacility(IEnumerable<Manifest> manifests)
        {
            var ids = string.Join(',', manifests.Select(x =>$"'{x.FacilityId}'"));
            var sitecode = manifests.Select(x =>$"'{x.SiteCode}'").FirstOrDefault();

            ExecSql(
                $@"
                    DECLARE @BatchSize INT = 50000; 
                    DECLARE @RowsAffected INT = 1;
                    WHILE @RowsAffected > 0
                    BEGIN
                        DELETE TOP (@BatchSize) {nameof(VmmcContext.VmmcEnrollmentExtracts)} WHERE {nameof(VmmcEnrollmentExtract.SiteCode)} = {sitecode};
                        SET @RowsAffected = @@ROWCOUNT;
                    END
                ", 3600);
            
            try{
            ExecSql(
                $@"        
                DECLARE @BatchSize INT = 50000; 
                    DECLARE @RowsAffected INT = 1;
                    WHILE @RowsAffected > 0
                    BEGIN
                        DELETE TOP (@BatchSize) {nameof(VmmcContext.VmmcProcedureExtracts)} WHERE {nameof(VmmcProcedureExtract.SiteCode)} = {sitecode};
                        SET @RowsAffected = @@ROWCOUNT;
                    END
                ", 3600);
            }
            catch (Exception e)
            {
                Log.Error( $"Clear ERROR {nameof(VmmcContext.VmmcProcedureExtracts)}"+ e);
            }
            
            try{
            ExecSql(
                $@" 
                DECLARE @BatchSize INT = 50000; 
                    DECLARE @RowsAffected INT = 1;
                    WHILE @RowsAffected > 0
                    BEGIN
                        DELETE TOP (@BatchSize) {nameof(VmmcContext.VmmcFollowupVisitExtracts)} WHERE {nameof(VmmcFollowupVisitExtract.SiteCode)} = {sitecode};
                        SET @RowsAffected = @@ROWCOUNT;
                    END
                ", 3600);
            }
            catch (Exception e)
            {
                Log.Error( $"Clear ERROR {nameof(VmmcContext.VmmcFollowupVisitExtracts)}"+ e);

            }
            
            try{
            ExecSql(
                $@" 
                DECLARE @BatchSize INT = 50000; 
                    DECLARE @RowsAffected INT = 1;
                    WHILE @RowsAffected > 0
                    BEGIN
                        DELETE TOP (@BatchSize) {nameof(VmmcContext.VmmcMhpeExtracts)} WHERE {nameof(VmmcMhpeExtract.SiteCode)} = {sitecode};
                        SET @RowsAffected = @@ROWCOUNT;
                    END
                ", 3600);
            }
            catch (Exception e)
            {
                Log.Error( $"Clear ERROR {nameof(VmmcContext.VmmcMhpeExtracts)}"+ e);
            }
            
            try{
            ExecSql(
                $@" 
                DECLARE @BatchSize INT = 50000; 
                    DECLARE @RowsAffected INT = 1;
                    WHILE @RowsAffected > 0
                    BEGIN
                        DELETE TOP (@BatchSize) {nameof(VmmcContext.VmmcDiscontinuationExtracts)} WHERE {nameof(VmmcDiscontinuationExtract.SiteCode)} = {sitecode} ;
                        SET @RowsAffected = @@ROWCOUNT;
                    END
                ", 3600);
            }
            catch (Exception e)
            {
                Log.Error( $"Clear ERROR {nameof(VmmcContext.VmmcDiscontinuationExtracts)}"+ e);
            }



            var mids = string.Join(',', manifests.Select(x => $"'{x.Id}'"));
            
            ExecSql(
                $@"
                UPDATE
                    {nameof(VmmcContext.Manifests)}
                SET
                    {nameof(Manifest.Status)}={(int)ManifestStatus.Processed},
                    {nameof(Manifest.StatusDate)}=GETDATE()
                WHERE
                    {nameof(Manifest.Id)} in ({mids})");
            
        }

        public void ClearFacility(IEnumerable<Manifest> manifests, string project)
        {
            var sitecode = string.Join(',', manifests.Select(x =>$"'{x.SiteCode}'"));

            var ids = string.Join(',', manifests.Select(x =>$"'{x.FacilityId}'"));
            ExecSql(
                $@"
                    DELETE FROM {nameof(VmmcContext.VmmcEnrollmentExtracts)} WHERE {nameof(VmmcEnrollmentExtract.SiteCode)} = {sitecode} AND {nameof(VmmcEnrollmentExtract.Project)}='{project}';                   
                    DELETE FROM {nameof(VmmcContext.VmmcProcedureExtracts)} WHERE {nameof(VmmcProcedureExtract.SiteCode)} = {sitecode} AND {nameof(VmmcProcedureExtract.Project)}='{project}';
                    DELETE FROM {nameof(VmmcContext.VmmcFollowupVisitExtracts)} WHERE {nameof(VmmcFollowupVisitExtract.SiteCode)} = {sitecode} AND {nameof(VmmcFollowupVisitExtract.Project)}='{project}';
                    DELETE FROM {nameof(VmmcContext.VmmcMhpeExtracts)} WHERE {nameof(VmmcMhpeExtract.SiteCode)} = {sitecode} AND {nameof(VmmcMhpeExtract.Project)}='{project}';
                    DELETE FROM {nameof(VmmcContext.VmmcDiscontinuationExtracts)} WHERE {nameof(VmmcDiscontinuationExtract.SiteCode)} = {sitecode} AND {nameof(VmmcDiscontinuationExtract.Project)}='{project}';
  
                 "
            );

            var mids = string.Join(',', manifests.Select(x => $"'{x.Id}'"));
            ExecSql(
                $@"
                    UPDATE
                        {nameof(VmmcContext.Manifests)}
                    SET
                        {nameof(Manifest.Status)}={(int)ManifestStatus.Processed},
                        {nameof(Manifest.StatusDate)}=GETDATE()
                    WHERE
                        {nameof(Manifest.Id)} in ({mids})");
        }

        public int GetPatientCount(Guid id)
        {
            var ctt = Context as VmmcContext;
            var cargo = ctt.Cargoes.FirstOrDefault(x => x.ManifestId == id && x.Type == CargoType.Patient);
            if (null != cargo)
                return cargo.Items.Split(",").Length;

            return 0;
        }

        public IEnumerable<Manifest> GetStaged(int siteCode)
        {
            var ctt = Context as VmmcContext;
            var manifests = DbSet.AsNoTracking().Where(x => x.Status == ManifestStatus.Staged && x.SiteCode == siteCode)
                .ToList();

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = ctt.Cargoes.AsNoTracking()
                    .Where(x => x.Type != CargoType.Patient && x.ManifestId == manifest.Id).ToList();
            }

            return manifests;
        }

        public async Task EndSession(Guid session)
        {
            var end = DateTime.Now;
            var sql = $"UPDATE {nameof(VmmcContext.Manifests)} SET [{nameof(Manifest.End)}]=@end WHERE [{nameof(Manifest.Session)}]=@session";
            await Context.Database.GetDbConnection().ExecuteAsync(sql, new {session, end});
        }

        public IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session)
        {
            var sql = $"SELECT * FROM {nameof(VmmcContext.Manifests)} WHERE [{nameof(Manifest.Session)}]=@session";
            var manifests = Context.Database.GetDbConnection().Query<Manifest>(sql,new{session}).ToList();
            return manifests.Select(x => new HandshakeDto()
            {
                Id = x.Id, End = x.End, Session = x.Session, Start = x.Start
            });
        }
        public void updateCount(Guid id, int clientCount)
        {
            var sql =
                $"UPDATE {nameof(VmmcContext.Manifests)} SET [{nameof(Manifest.Recieved)}]=@clientCount WHERE [{nameof(Manifest.Id)}]=@id";
            Context.Database.GetDbConnection().Execute(sql, new { id, clientCount });
        }
        
        public string GetDWAPIversionSending(int siteCode)
        {
            var ctt = Context as VmmcContext;
            var manifests = DbSet.AsNoTracking().Where(x => x.Status == ManifestStatus.Staged && x.SiteCode == siteCode)
                .ToList();
            // DbSet.AsNoTracking().FacMetrics.Select(o => o.Metric).Where(x => x.Contains("CareTreatment")).ToList()[0]
        
            foreach (var manifest in manifests)
            {
                manifest.Cargoes = ctt.Cargoes.AsNoTracking()
                    .Where(x => x.Type != CargoType.Patient && x.ManifestId == manifest.Id).ToList();
            }
            var version = manifests.Select(o => o.Cargoes).Select(x =>  x.Where(m => m.Items.Contains("VmmcService"))).FirstOrDefault().ToList()[0].Items;
            
            return JObject.Parse(version)["Version"].ToString();
        }
        
    }
}
