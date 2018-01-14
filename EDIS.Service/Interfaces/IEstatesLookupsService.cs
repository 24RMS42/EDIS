using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain.EstatesLookups;

namespace EDIS.Service.Interfaces
{
    public interface IEstatesLookupsService
    {
        Task<string> FetchEstateLookups(string estateId, bool forceCloud = false);
        Task<IEnumerable<ObservationFrom>> GetObservationFromLookups(string estateId, bool forceCloud = false);
        Task<IEnumerable<ObservationGroup>> GetObservationGroupLookups(string estateId, bool forceCloud = false);
    }
}