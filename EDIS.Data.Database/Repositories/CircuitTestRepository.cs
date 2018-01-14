using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Circuits;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class CircuitTestRepository : Repository<CircuitTest>, ICircuitTestRepository
    {
        public async Task<CircuitTest> GetCircuitTest(string circuitTestId)
        {
            var board = await Connection.Table<CircuitTest>().Where(x => x.CircuitTestId == circuitTestId).FirstOrDefaultAsync();
            if (board != null)
                return await Connection.GetWithChildrenAsync<CircuitTest>(circuitTestId);
            return null;
        }

        public async Task UpdateCircuit(CircuitTest circuit)
        {
            var b = await Connection.Table<CircuitTest>().Where(x => x.CircuitTestId == circuit.CircuitTestId).FirstOrDefaultAsync();
            if (b != null)
                await Connection.InsertOrReplaceWithChildrenAsync(circuit, true);
        }
    }
}