using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Circuits;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface ICircuitRepository : IRepository<Circuit>
    {
        Task<Circuit> GetCircuit(string circuitId);
        Task UpdateCircuit(Circuit circuit);
    }
}