using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain.Buildings;

namespace EDIS.Service.Interfaces
{
    public interface IBuildingsService
    {
        Task<ServiceResult> GetAllBuildings(string estateId, bool forceCloud = false);
    }
}