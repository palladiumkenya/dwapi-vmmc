using System.Threading.Tasks;
using Dwapi.Vmmc.Core.Domain;
using Dwapi.Vmmc.SharedKernel.Interfaces;

namespace Dwapi.Vmmc.Core.Interfaces.Repository
{
    public interface IDocketRepository : IRepository<Docket, string>
    {
       Task<Docket> FindAsync(string docket);
    }
}