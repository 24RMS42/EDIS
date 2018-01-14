using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Circuits;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface ICircuitTestRepository : IRepository<CircuitTest>
    {
        Task<CircuitTest> GetCircuitTest(string circuitTestId);
        Task UpdateCircuit(CircuitTest circuit);
    }
}