using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Circuits;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface ICircuitPointsRcdTestRepository : IRepository<CircuitPointsRcdTest>
    {
        Task<CircuitPointsRcdTest> GetCircuitTestedEndPoint(string endPointId);
        Task<List<CircuitPointsRcdTest>> GetCircuitTestedEndPoints(string circuitId);
        Task UpdateCircuitTestedEndPoint(CircuitPointsRcdTest endPoint);
        Task DeleteCircuitTestedEndPoint(string endPointId);
    }
}