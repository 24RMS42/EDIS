using EDIS.Data.Database.Repositories.Interfaces;

namespace EDIS.Data.Database.DatabaseManager
{
    public interface IDatabaseManager
    {
        IBoardRepository BoardRepository { get; }
        IBoardRowRepository BoardRowRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IBuildingUserRepository BuildingUserRepository { get; }
        ICertificateRepository CertificateRepository { get; }
        ICertificateRowRepository CertificateRowRepository { get; }
        ICircuitRepository CircuitRepository { get; }
        IEstateRepository EstateRepository { get; }
        IBuildingRowRepository BuildingRowRepository { get; }
        IUserRepository UserRepository { get; }
        IInstrumentRepository InstrumentRepository { get; }
        ICertificateInspectionRepository CertificateInspectionRepository { get; }
        IBuildingContactTestRepository BuildingContactTestRepository { get; }
        IBuildingTestRepository BuildingTestRepository { get; }
        IBoardTestRepository BoardTestRepository { get; }
        ICircuitTestRepository CircuitTestRepository { get; }
        ICircuitPointsRcdTestRepository CircuitPointsRcdTestRepository { get; }
        ICertificateInspectionObservationsRepository CertificateInspectionObservationsRepository { get; }
        ICircuitPointsRcdRepository CircuitPointsRcdRepository { get; }
		IObservationFromRepository ObservationFromRepository { get; }
        IObservationGroupRepository ObservationGroupRepository { get; }
        IBoardReferenceTypesRepository BoardReferenceTypesRepository { get; }
        IConductorSizeTypesRepository ConductorSizeTypesRepository { get; }
        ICircuitOptsRepository CircuitOptsRepository { get; }
        IOptsRepository OptsRepository { get; }
        IPhasesRepository PhasesRepository { get; }
        IPhaseSortOrdersRepository PhaseSortOrdersRepository { get; }
        IRatingsRepository RatingsRepository { get; }
        ICableReferenceMethodsRepository CableReferenceMethodsRepository { get; }
        ICertificateInspectionQuestionsRepository CertificateInspectionQuestionsRepository { get; }
        IBsAmendmentDatesRepository BsAmendmentDatesRepository { get; }
        IRcdTypeRepository RcdTypeRepository { get; }
        IRcdOpCurrentsRepository RcdOpCurrentsRepository { get; }
        ICsaCpcRepository CsaCpcRepository { get; }
        ICsaLiveRepository CsaLiveRepository { get; }
        INamingConventionRepository NamingConventionRepository { get; }
    }
}