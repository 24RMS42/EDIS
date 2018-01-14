using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain.Buildings;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;

namespace EDIS.Service.Implementation
{
    public class BuildingsService : BaseService, IBuildingsService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<ServiceResult> GetAllBuildings(string estateId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var buildingsDb = await DbManager.BuildingRowRepository.GetAll(x => x.EstateId == estateId);
                if (buildingsDb.Any())
                    return new SuccessServiceResult {ResultObject = buildingsDb};
            }

            var url = GlobalSettings.BaseURL + "/buildings";

            var response = await _requestProvider.PostAsync<BuildingsRequest, BuildingsResponse>(url, new BuildingsRequest { Token = Settings.AccessToken, EstateId = estateId});

            if (response != null && response.Buildings.Any())
            {
                foreach (var building in response.Buildings)
                {
                    building.EstateId = estateId;
                }

                if (forceCloud)
                    await DbManager.BuildingRowRepository.DeleteAll();

                await DbManager.BuildingRowRepository.AddManyOrReplace(response.Buildings);

                return new SuccessServiceResult { ResultObject = response.Buildings };
            }

            return new FalseServiceResult("Response is null");
        }

        public BuildingsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
    }
}