using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain.Buildings;

namespace EDIS.Service.Interfaces
{
    public interface IBuildingService
    {
        Task<string> FetchBuldingDetail(string buildingId, bool forceCloud = false);
        Task<IEnumerable<BuildingUser>> GetBuildingUsers(string buildingId, bool forceCloud = false);
    }
}
