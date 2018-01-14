using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Profile;
using EDIS.Shared.Helpers;
using System;
using System.Threading.Tasks;
using EDIS.Core;
using System.Text.RegularExpressions;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class InstrumentRepository : Repository<Instrument>, IInstrumentRepository
    {

        public async Task<Instrument> GetInstrument(string userId)
        {
            var instrument = await Connection.Table<Instrument>().Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (instrument != null)
                return await Connection.GetWithChildrenAsync<Instrument>(userId);
            return null;
        }

        public async Task UpdateInstrument(Instrument instrument)
        {
            var instrumentOld = await GetInstrument(instrument.UserId);
            if (instrumentOld != null)
            {
                await Connection.UpdateWithChildrenAsync(instrument);
            }
        }

    }

   
}
