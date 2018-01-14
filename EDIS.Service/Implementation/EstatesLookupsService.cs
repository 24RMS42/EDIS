using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain.EstatesLookups;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;

namespace EDIS.Service.Implementation
{
    public class EstatesLookupsService : BaseService, IEstatesLookupsService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<string> FetchEstateLookups(string estateId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var ObservationFromDb = await DbManager.ObservationFromRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationFromDb.Any())
                    return ("SUCCESS").ToString();

                var ObservationGroupDb = await DbManager.ObservationGroupRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationGroupDb.Any())
                    return ("SUCCESS").ToString();
            }

            var url = GlobalSettings.BaseURL + "/estates/lookups";

            var response = await _requestProvider.PostAsync<EstatesLookupsRequest, EstatesLookupsResponse>(url, new EstatesLookupsRequest { Token = Settings.AccessToken, EstateId = estateId });

            if (response.ObservationFromEncloser != null && response.ObservationFromEncloser.ObservationFromItems.Count > 0)
            {
                if (forceCloud)
                    await DbManager.ObservationFromRepository.DeleteByQuery(x => x.EstateId == estateId);

                await DbManager.ObservationFromRepository.AddMany(response.ObservationFromEncloser.ObservationFromItems);
            }
            if (response.ObservationGroupEncloser != null && response.ObservationGroupEncloser.ObservationGroupItems.Count > 0)
            {
                if (forceCloud)
                    await DbManager.ObservationGroupRepository.DeleteByQuery(x => x.EstateId == estateId);

                await DbManager.ObservationGroupRepository.AddMany(response.ObservationGroupEncloser.ObservationGroupItems);
            }

            return ("SUCCESS").ToString();
        }

        public EstatesLookupsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<ObservationFrom>> GetObservationFromLookups(string estateId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var ObservationFromDb = await DbManager.ObservationFromRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationFromDb.Any())
                    return ObservationFromDb;
            }

            string res = await FetchEstateLookups(estateId, true);
            if (res.Equals("SUCCESS"))
            {
                var ObservationFromDb = await DbManager.ObservationFromRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationFromDb.Any())
                    return ObservationFromDb;
            }

            return null;
        }

        public async Task<IEnumerable<ObservationGroup>> GetObservationGroupLookups(string estateId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var ObservationGroupDb = await DbManager.ObservationGroupRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationGroupDb.Any())
                    return ObservationGroupDb;
            }

            string res = await FetchEstateLookups(estateId, true);
            if (res.Equals("SUCCESS"))
            {
                var ObservationGroupDb = await DbManager.ObservationGroupRepository.GetAll(x => x.EstateId == estateId);
                if (ObservationGroupDb.Any())
                    return ObservationGroupDb;
            }

            return null;
        }
    }
}
