using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain.Estates;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;

namespace EDIS.Service.Implementation
{
    public class EstatesService : BaseService, IEstatesService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<ServiceResult> GetAllEstates(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var estatesDb = await DbManager.EstateRepository.GetAll();
                if (estatesDb.Any())
                    return new SuccessServiceResult { ResultObject = estatesDb };
            }

            var url = GlobalSettings.BaseURL + "/estates";

            var response = await _requestProvider.PostAsync<BaseRequest, EstatesResponse>(url, new BaseRequest { Token = Settings.AccessToken });

            if (response != null && response.Estates.Any())
            {
                if (forceCloud)
                    await DbManager.EstateRepository.DeleteAll();
                await DbManager.EstateRepository.AddMany(response.Estates);

                return new SuccessServiceResult { ResultObject = response.Estates };
            }

            return new FalseServiceResult("Response is null");
        }

        public EstatesService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
    }
}