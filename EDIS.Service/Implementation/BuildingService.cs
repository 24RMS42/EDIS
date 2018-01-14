using System;
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
    public class BuildingService : BaseService, IBuildingService
    {
        private readonly IRequestProvider _requestProvider;

        /* Currently it only saves Building Users to local datasbase */
        public async Task<string> FetchBuldingDetail(string buildingId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var BuildingUserDb = await DbManager.BuildingUserRepository.GetAll(x => x.BuildingId == buildingId);
                if (BuildingUserDb.Any())
                    return ("SUCCESS").ToString();

            }

            var url = GlobalSettings.BaseURL + "/buildings/detail";

            var response = await _requestProvider.PostAsync<BuildingRequest, BuildingResponse>(url, new BuildingRequest { Token = Settings.AccessToken, BuildingId = buildingId });

            if (response.BuildingId != null && response.BuildingId.Equals(buildingId) && response.Buildings != null)
            {
                var BuildingDb = await DbManager.BuildingUserRepository.GetAll(x => x.BuildingId == buildingId);
                if (!(BuildingDb.Any()))
                {
                    Building building = new Building();
                    await DbManager.BuildingRepository.Add(response.Buildings.FirstOrDefault());
                }
            }

            if (response.BuildingUsers != null && response.BuildingUsers.Count > 0)
            {
                await DbManager.BuildingUserRepository.DeleteByQuery(x => x.BuildingId == buildingId);

                List<BuildingUser> buildingUsers = response.BuildingUsers;
                for (var i = 0; i < buildingUsers.Count; i++)
                {
                    buildingUsers[i].BuildingId = buildingId;
                }

                await DbManager.BuildingUserRepository.AddMany(buildingUsers);
            }
            

            return ("SUCCESS").ToString();
        }

        public BuildingService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<BuildingUser>> GetBuildingUsers(string buildingId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var BuildingUserDb = await DbManager.BuildingUserRepository.GetAll(x => x.BuildingId == buildingId);
                if (BuildingUserDb.Any())
                    return BuildingUserDb;
            }

            string res = await FetchBuldingDetail(buildingId, true);
            if (res.Equals("SUCCESS"))
            {
                var BuildingUserDb = await DbManager.BuildingUserRepository.GetAll(x => x.BuildingId == buildingId);
                if (BuildingUserDb.Any())
                    return BuildingUserDb;
            }

            return null;
        }

        /*private Building MapBuildingFields(Building building, BuildingResponse buildingResponse)
        {
            building.BuildingId = buildingResponse.BuildingId;
            building.EstateId = buildingResponse.es;
            building.UserOrganisation = buildingResponse.UserOrganisation;
            building.BuildingName = buildingResponse.BuildingName;
            building.UserAddress = buildingResponse.UserAddress;
            building.UserMobile = buildingResponse.UserMobile;
            building.UserLandline = buildingResponse.UserLandline;
            building.UserWebsite = buildingResponse.UserWebsite;
            building.UserSubscribedRpts = buildingResponse.UserSubscribedRpts;

            return building;
        }*/

    }
}
