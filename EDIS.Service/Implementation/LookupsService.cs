using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain.Lookups;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;

namespace EDIS.Service.Implementation
{
    public class LookupsService : BaseService, ILookupsService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<string> FetchLookups(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var BoardReferenceTypesDb = await DbManager.BoardReferenceTypesRepository.GetAll();
                if (BoardReferenceTypesDb.Any())
                    return ("SUCCESS").ToString();

                var ConductorSizeTypesDb = await DbManager.ConductorSizeTypesRepository.GetAll();
                if (ConductorSizeTypesDb.Any())
                    return ("SUCCESS").ToString();

                var CircuitOptsDb = await DbManager.CircuitOptsRepository.GetAll();
                if (CircuitOptsDb.Any())
                    return ("SUCCESS").ToString();

                var OptsDb = await DbManager.OptsRepository.GetAll();
                if (OptsDb.Any())
                    return ("SUCCESS").ToString();

                var PhasesDb = await DbManager.PhasesRepository.GetAll();
                if (PhasesDb.Any())
                    return ("SUCCESS").ToString();

                var PhaseSortOrdersDb = await DbManager.PhaseSortOrdersRepository.GetAll();
                if (PhaseSortOrdersDb.Any())
                    return ("SUCCESS").ToString();

                var RatingsDb = await DbManager.RatingsRepository.GetAll();
                if (RatingsDb.Any())
                    return ("SUCCESS").ToString();

                var CableReferenceMethodsDb = await DbManager.CableReferenceMethodsRepository.GetAll();
                if (CableReferenceMethodsDb.Any())
                    return ("SUCCESS").ToString();

                var CertificateInspectionQuestionsDb = await DbManager.CertificateInspectionQuestionsRepository.GetAll();
                if (CertificateInspectionQuestionsDb.Any())
                    return ("SUCCESS").ToString();

                var BsAmendmentDatesDb = await DbManager.BsAmendmentDatesRepository.GetAll();
                if (BsAmendmentDatesDb.Any())
                    return ("SUCCESS").ToString();
            }

            var url = GlobalSettings.BaseURL + "/lookups";

            var response = await _requestProvider.PostAsync<BaseRequest, LookupsResponse>(url, new BaseRequest { Token = Settings.AccessToken });

            if (response != null && response.Lookups != null)
            {
                if (forceCloud)
                {
                    await DbManager.BoardReferenceTypesRepository.DeleteAll();
                    await DbManager.ConductorSizeTypesRepository.DeleteAll();
                    await DbManager.CircuitOptsRepository.DeleteAll();
                    await DbManager.OptsRepository.DeleteAll();
                    await DbManager.PhasesRepository.DeleteAll();
                    await DbManager.PhaseSortOrdersRepository.DeleteAll();
                    await DbManager.RatingsRepository.DeleteAll();
                    await DbManager.CableReferenceMethodsRepository.DeleteAll();
                    await DbManager.CertificateInspectionQuestionsRepository.DeleteAll();
                    await DbManager.BsAmendmentDatesRepository.DeleteAll();
                    await DbManager.RcdTypeRepository.DeleteAll();
                    await DbManager.RcdOpCurrentsRepository.DeleteAll();
                    await DbManager.CsaCpcRepository.DeleteAll();
                    await DbManager.CsaLiveRepository.DeleteAll();
                    await DbManager.NamingConventionRepository.DeleteAll();
                }

                await DbManager.BoardReferenceTypesRepository.AddMany(response.Lookups.BoardReferenceTypes);
                await DbManager.ConductorSizeTypesRepository.AddMany(response.Lookups.ConductorSizeTypes);
                await DbManager.CircuitOptsRepository.AddMany(response.Lookups.CircuitOpts);
                await DbManager.OptsRepository.AddMany(response.Lookups.Opts);
                await DbManager.PhasesRepository.AddMany(response.Lookups.Phases);
                await DbManager.PhaseSortOrdersRepository.AddMany(response.Lookups.PhaseSortOrders);
                await DbManager.RatingsRepository.AddMany(response.Lookups.Ratings);
                await DbManager.CableReferenceMethodsRepository.AddMany(response.Lookups.CableReferenceMethods);
                await DbManager.CertificateInspectionQuestionsRepository.AddMany(response.Lookups.CertificateInspectionQuestions);
                await DbManager.BsAmendmentDatesRepository.AddMany(response.Lookups.BsAmendmentDates);
                await DbManager.RcdTypeRepository.AddMany(response.Lookups.RcdTypes);
                await DbManager.RcdOpCurrentsRepository.AddMany(response.Lookups.RcdOpCurrents);
                await DbManager.CsaCpcRepository.AddMany(response.Lookups.CsaCpcs);
                await DbManager.CsaLiveRepository.AddMany(response.Lookups.CsaLives);
                await DbManager.NamingConventionRepository.AddMany(response.Lookups.NamingConventions);

                return ("SUCCESS").ToString();
            }

            return ("FAIL").ToString();
        }

        public LookupsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<BoardReferenceTypes>> GetBoardReferenceTypes(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var BoardReferenceTypesDb = await DbManager.BoardReferenceTypesRepository.GetAll();
                if (BoardReferenceTypesDb.Any())
                    return BoardReferenceTypesDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var BoardReferenceTypesDb = await DbManager.BoardReferenceTypesRepository.GetAll();
                if (BoardReferenceTypesDb.Any())
                    return BoardReferenceTypesDb;
            }

            return null;
        }

        public async Task<IEnumerable<ConductorSizeTypes>> GetConductorSizeTypes(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var ConductorSizeTypesDb = await DbManager.ConductorSizeTypesRepository.GetAll();
                if (ConductorSizeTypesDb.Any())
                    return ConductorSizeTypesDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var ConductorSizeTypesDb = await DbManager.ConductorSizeTypesRepository.GetAll();
                if (ConductorSizeTypesDb.Any())
                    return ConductorSizeTypesDb;
            }

            return null;
        }

        public async Task<IEnumerable<CircuitOpts>> GetCircuitOpts(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var CircuitOptsDb = await DbManager.CircuitOptsRepository.GetAll();
                if (CircuitOptsDb.Any())
                    return CircuitOptsDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var CircuitOptsDb = await DbManager.CircuitOptsRepository.GetAll();
                if (CircuitOptsDb.Any())
                    return CircuitOptsDb;
            }

            return null;
        }

        public async Task<IEnumerable<Opts>> GetOpts(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var OptsDb = await DbManager.OptsRepository.GetAll();
                if (OptsDb.Any())
                    return OptsDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var OptsDb = await DbManager.OptsRepository.GetAll();
                if (OptsDb.Any())
                    return OptsDb;
            }

            return null;
        }

        public async Task<IEnumerable<Phases>> GetPhases(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var PhasesDb = await DbManager.PhasesRepository.GetAll();
                if (PhasesDb.Any())
                    return PhasesDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var PhasesDb = await DbManager.PhasesRepository.GetAll();
                if (PhasesDb.Any())
                    return PhasesDb;
            }

            return null;
        }

        public async Task<IEnumerable<PhaseSortOrders>> GetPhaseSortOrders(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var PhaseSortOrdersDb = await DbManager.PhaseSortOrdersRepository.GetAll();
                if (PhaseSortOrdersDb.Any())
                    return PhaseSortOrdersDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var PhaseSortOrdersDb = await DbManager.PhaseSortOrdersRepository.GetAll();
                if (PhaseSortOrdersDb.Any())
                    return PhaseSortOrdersDb;
            }

            return null;
        }

        public async Task<IEnumerable<Ratings>> GetRatings(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var RatingsDb = await DbManager.RatingsRepository.GetAll();
                if (RatingsDb.Any())
                    return RatingsDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var RatingsDb = await DbManager.RatingsRepository.GetAll();
                if (RatingsDb.Any())
                    return RatingsDb;
            }

            return null;
        }

        public async Task<IEnumerable<CableReferenceMethods>> GetCableReferenceMethods(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var CableReferenceMethodsDb = await DbManager.CableReferenceMethodsRepository.GetAll();
                if (CableReferenceMethodsDb.Any())
                    return CableReferenceMethodsDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var CableReferenceMethodsDb = await DbManager.CableReferenceMethodsRepository.GetAll();
                if (CableReferenceMethodsDb.Any())
                    return CableReferenceMethodsDb;
            }

            return null;
        }

        public async Task<IEnumerable<CertificateInspectionQuestions>> GetCertificateInspectionQuestions(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var CertificateInspectionQuestionsDb = await DbManager.CertificateInspectionQuestionsRepository.GetAll();
                if (CertificateInspectionQuestionsDb.Any())
                    return CertificateInspectionQuestionsDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var CertificateInspectionQuestionsDb = await DbManager.CertificateInspectionQuestionsRepository.GetAll();
                if (CertificateInspectionQuestionsDb.Any())
                    return CertificateInspectionQuestionsDb;
            }

            return null;
        }

        public async Task<IEnumerable<BsAmendmentDates>> GetBsAmendmentDates(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var BsAmendmentDatesDb = await DbManager.BsAmendmentDatesRepository.GetAll();
                if (BsAmendmentDatesDb.Any())
                    return BsAmendmentDatesDb;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var BsAmendmentDatesDb = await DbManager.BsAmendmentDatesRepository.GetAll();
                if (BsAmendmentDatesDb.Any())
                    return BsAmendmentDatesDb;
            }

            return null;
        }

        public async Task<IEnumerable<RcdTypes>> GetRcdTypes(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var rcdTypes = await DbManager.RcdTypeRepository.GetAll();
                if (rcdTypes.Any())
                    return rcdTypes;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var rcdTypes = await DbManager.RcdTypeRepository.GetAll();
                if (rcdTypes.Any())
                    return rcdTypes;
            }

            return null;
        }

        public async Task<IEnumerable<RcdOpCurrents>> GetRcdOpCurrents(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var rcdOpCurrents = await DbManager.RcdOpCurrentsRepository.GetAll();
                if (rcdOpCurrents.Any())
                    return rcdOpCurrents;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var rcdOpCurrents = await DbManager.RcdOpCurrentsRepository.GetAll();
                if (rcdOpCurrents.Any())
                    return rcdOpCurrents;
            }

            return null;
        }

        public async Task<IEnumerable<CsaCpc>> GetCsaCpcs(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var csaCpcs = await DbManager.CsaCpcRepository.GetAll();
                if (csaCpcs.Any())
                    return csaCpcs;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var csaCpcs = await DbManager.CsaCpcRepository.GetAll();
                if (csaCpcs.Any())
                    return csaCpcs;
            }

            return null;
        }

        public async Task<IEnumerable<CsaLive>> GetCsaLives(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var csaLives = await DbManager.CsaLiveRepository.GetAll();
                if (csaLives.Any())
                    return csaLives;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var csaLives = await DbManager.CsaLiveRepository.GetAll();
                if (csaLives.Any())
                    return csaLives;
            }

            return null;
        }

        public async Task<IEnumerable<NamingConventions>> GetNamingConventions(bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var namingConventions = await DbManager.NamingConventionRepository.GetAll();
                if (namingConventions.Any())
                    return namingConventions;
            }

            string res = await FetchLookups(true);
            if (res.Equals("SUCCESS"))
            {
                var namingConventions = await DbManager.NamingConventionRepository.GetAll();
                if (namingConventions.Any())
                    return namingConventions;
            }

            return null;
        }

    }
}
