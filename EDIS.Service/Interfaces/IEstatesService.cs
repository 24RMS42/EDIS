using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain.Estates;

namespace EDIS.Service.Interfaces
{
    public interface IEstatesService
    {
        Task<ServiceResult> GetAllEstates(bool forceCloud = false);
    }
}