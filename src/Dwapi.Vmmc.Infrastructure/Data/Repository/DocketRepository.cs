using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.Core.Interfaces.Repository;
using Dwapi.Vmmc.Infrastructure.Data;
using Dwapi.Vmmc.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.Vmmc.Infrastructure.Data.Repository
{
    public class DocketRepository : BaseRepository<Docket, string>, IDocketRepository
    {
        public DocketRepository(VmmcContext context) : base(context)
        {
        }
        public Task<Docket> FindAsync(string docket)
        {
           var ctx=Context as VmmcContext;
            return ctx.Dockets.Include(x => x.Subscribers).AsTracking().FirstOrDefaultAsync(x => x.Id == docket);
        }
    }
}
