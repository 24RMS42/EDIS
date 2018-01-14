using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain.Lookups;

namespace EDIS.Service.Interfaces
{
    public interface ILookupsService
    {
        Task<string> FetchLookups(bool forceCloud = false);
        Task<IEnumerable<BoardReferenceTypes>> GetBoardReferenceTypes(bool forceCloud = false);
        Task<IEnumerable<ConductorSizeTypes>> GetConductorSizeTypes(bool forceCloud = false);
        Task<IEnumerable<CircuitOpts>> GetCircuitOpts(bool forceCloud = false);
        Task<IEnumerable<Opts>> GetOpts(bool forceCloud = false);
        Task<IEnumerable<Phases>> GetPhases(bool forceCloud = false);
        Task<IEnumerable<PhaseSortOrders>> GetPhaseSortOrders(bool forceCloud = false);
        Task<IEnumerable<Ratings>> GetRatings(bool forceCloud = false);
        Task<IEnumerable<CableReferenceMethods>> GetCableReferenceMethods(bool forceCloud = false);
        Task<IEnumerable<CertificateInspectionQuestions>> GetCertificateInspectionQuestions(bool forceCloud = false);
        Task<IEnumerable<BsAmendmentDates>> GetBsAmendmentDates(bool forceCloud = false);
        Task<IEnumerable<RcdTypes>> GetRcdTypes(bool forceCloud = false);
        Task<IEnumerable<RcdOpCurrents>> GetRcdOpCurrents(bool forceCloud = false);
        Task<IEnumerable<CsaCpc>> GetCsaCpcs(bool forceCloud = false);
        Task<IEnumerable<CsaLive>> GetCsaLives(bool forceCloud = false);
        Task<IEnumerable<NamingConventions>> GetNamingConventions(bool forceCloud = false);
    }
}
