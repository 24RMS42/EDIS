using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Circuits;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class CircuitRepository : Repository<Circuit>, ICircuitRepository
    {
        public async Task<Circuit> GetCircuit(string circuitId)
        {
            var cert = await Connection.Table<Circuit>().Where(x => x.CircuitId == circuitId).FirstOrDefaultAsync();
            if (cert != null)
                return await Connection.GetWithChildrenAsync<Circuit>(circuitId);
            return null;
        }

        public async Task UpdateCircuit(Circuit circuit)
        {
            var b = await Connection.Table<Circuit>().Where(x => x.CircuitId == circuit.CircuitId).FirstOrDefaultAsync();
            if (b != null)
                await Connection.InsertOrReplaceWithChildrenAsync(circuit, true);
        }
    }
}