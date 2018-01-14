using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Circuits;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class CircuitPointsRcdTestRepository : Repository<CircuitPointsRcdTest>, ICircuitPointsRcdTestRepository
    {
        public async Task<CircuitPointsRcdTest> GetCircuitTestedEndPoint(string endPointId)
        {
            var cert = await Connection.Table<CircuitPointsRcdTest>().Where(x => x.CprTestId == endPointId).FirstOrDefaultAsync();
            if (cert != null)
                return await Connection.GetWithChildrenAsync<CircuitPointsRcdTest>(endPointId);
            return null;
        }

        public async Task<List<CircuitPointsRcdTest>> GetCircuitTestedEndPoints(string circuitId)
        {
            var cert = await Connection.Table<CircuitPointsRcdTest>().Where(x => x.CircuitId == circuitId).ToListAsync();
            if (cert != null && cert.Any())
                return await Connection.GetAllWithChildrenAsync<CircuitPointsRcdTest>(x => x.CircuitId == circuitId);//WithChildrenAsync<List<CircuitPointsRcdTest>>(circuitId);
            return null;
        }

        public async Task UpdateCircuitTestedEndPoint(CircuitPointsRcdTest endPoint)
        {
            //var cert = await GetCircuitTestedEndPoint(endPoint.CprTestId);
            //if (cert != null)
            await Connection.InsertOrReplaceWithChildrenAsync(endPoint);
        }

        public async Task DeleteCircuitTestedEndPoint(string endPointId)
        {
            var point = await GetCircuitTestedEndPoint(endPointId);
            if (point != null)
                await Connection.DeleteAsync(point, true);
        }
    }
}