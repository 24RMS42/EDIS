using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Profile;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface IInstrumentRepository : IRepository<Instrument>
    {
        Task<Instrument> GetInstrument(string userId);
        Task UpdateInstrument(Instrument instrument);
    }
}
