using EDIS.Data.Database.Repositories;
using EDIS.Data.Database.Repositories.Interfaces;

namespace EDIS.Data.Database.DatabaseManager
{
    public class DatabaseManager : IDatabaseManager
    {
        private IBoardRepository _boardRepository;
        private IBoardRowRepository _boardRowRepository;
        private IBuildingRepository _buildingRepository;
        private IBuildingUserRepository _buildingUserRepository;
        private ICertificateRepository _certificateRepository;
        private ICertificateRowRepository _certificateRowRepository;
        private ICircuitRepository _circuitRepository;
        private IEstateRepository _estateRepository;
        private IBuildingRowRepository _buildingRowRepository;
        private IUserRepository _userRepository;
        private IInstrumentRepository _instrumentRepository;
        private ICertificateInspectionRepository _certificateInspectionRepository;
        private IBuildingContactTestRepository _buildingContactTestRepository;
        private IBuildingTestRepository _buildingTestRepository;
        private IBoardTestRepository _boardTestRepository;
        private ICircuitTestRepository _circuitTestRepository;
        private ICircuitPointsRcdTestRepository _circuitPointsRcdTestRepository;
        private ICertificateInspectionObservationsRepository _certificateInspectionObservationsRepository;
        private ICircuitPointsRcdRepository _circuitPointsRcdRepository;
        private IObservationFromRepository _observationFromRepository;
        private IObservationGroupRepository _observationGroupRepository;
        private IBoardReferenceTypesRepository _boardReferenceTypesRepository;
        private IConductorSizeTypesRepository _conductorSizeTypesRepository;
        private ICircuitOptsRepository _circuitOptsRepository;
        private IOptsRepository _optsRepository;
        private IPhasesRepository _phasesRepository;
        private IPhaseSortOrdersRepository _phaseSortOrdersRepository;
        private IRatingsRepository _ratingsRepository;
        private ICableReferenceMethodsRepository _cableReferenceMethodsRepository;
        private ICertificateInspectionQuestionsRepository _certificateInspectionQuestionsRepository;
        private IBsAmendmentDatesRepository _bsAmendmentDatesRepository;
        private IRcdTypeRepository _rcdTypeRepository;
        private IRcdOpCurrentsRepository _rcdOpCurrentsRepository;
        private ICsaCpcRepository _csaCpcRepository;
        private ICsaLiveRepository _csaLiveRepository;
        private INamingConventionRepository _namingConventionRepository;

        public IBoardRepository BoardRepository => _boardRepository ?? (_boardRepository = new BoardRepository());
        public IBoardRowRepository BoardRowRepository => _boardRowRepository ?? (_boardRowRepository = new BoardRowRepository());
        public IBuildingRepository BuildingRepository => _buildingRepository ?? (_buildingRepository = new BuildingRepository());
        public IBuildingUserRepository BuildingUserRepository => _buildingUserRepository ?? (_buildingUserRepository = new BuildingUserRepository());
        public ICertificateRepository CertificateRepository => _certificateRepository ?? (_certificateRepository = new CertificateRepository());
        public ICertificateRowRepository CertificateRowRepository => _certificateRowRepository ?? (_certificateRowRepository = new CertificateRowRepository());
        public ICircuitRepository CircuitRepository => _circuitRepository ?? (_circuitRepository = new CircuitRepository());
        public IEstateRepository EstateRepository => _estateRepository ?? (_estateRepository = new EstateRepository());
        public IBuildingRowRepository BuildingRowRepository => _buildingRowRepository ?? (_buildingRowRepository = new BuildingRowRepository());
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository());
        public IInstrumentRepository InstrumentRepository => _instrumentRepository ?? (_instrumentRepository = new InstrumentRepository());
        public ICertificateInspectionRepository CertificateInspectionRepository => _certificateInspectionRepository ?? (_certificateInspectionRepository = new CertificateInspectionRepository());
        public IBuildingContactTestRepository BuildingContactTestRepository => _buildingContactTestRepository ?? (_buildingContactTestRepository = new BuildingContactTestRepository());
        public IBuildingTestRepository BuildingTestRepository => _buildingTestRepository ?? (_buildingTestRepository = new BuildingTestRepository());
        public IBoardTestRepository BoardTestRepository => _boardTestRepository ?? (_boardTestRepository = new BoardTestRepository());
        public ICircuitTestRepository CircuitTestRepository => _circuitTestRepository ?? (_circuitTestRepository = new CircuitTestRepository());
        public ICircuitPointsRcdTestRepository CircuitPointsRcdTestRepository => _circuitPointsRcdTestRepository ?? (_circuitPointsRcdTestRepository = new CircuitPointsRcdTestRepository());
        public ICertificateInspectionObservationsRepository CertificateInspectionObservationsRepository => _certificateInspectionObservationsRepository ?? (_certificateInspectionObservationsRepository = new CertificateInspectionObservationsRepository());
        public ICircuitPointsRcdRepository CircuitPointsRcdRepository => _circuitPointsRcdRepository ?? (_circuitPointsRcdRepository = new CircuitPointsRcdRepository());
        public IObservationFromRepository ObservationFromRepository => _observationFromRepository ?? (_observationFromRepository = new ObservationFromRepository());
        public IObservationGroupRepository ObservationGroupRepository => _observationGroupRepository ?? (_observationGroupRepository = new ObservationGroupRepository());
        public IBoardReferenceTypesRepository BoardReferenceTypesRepository => _boardReferenceTypesRepository ?? (_boardReferenceTypesRepository = new BoardReferenceTypesRepository());
        public IConductorSizeTypesRepository ConductorSizeTypesRepository => _conductorSizeTypesRepository ?? (_conductorSizeTypesRepository = new ConductorSizeTypesRepository());
        public ICircuitOptsRepository CircuitOptsRepository => _circuitOptsRepository ?? (_circuitOptsRepository = new CircuitOptsRepository());
        public IOptsRepository OptsRepository => _optsRepository ?? (_optsRepository = new OptsRepository());
        public IPhasesRepository PhasesRepository => _phasesRepository ?? (_phasesRepository = new PhasesRepository());
        public IPhaseSortOrdersRepository PhaseSortOrdersRepository => _phaseSortOrdersRepository ?? (_phaseSortOrdersRepository = new PhaseSortOrdersRepository());
        public IRatingsRepository RatingsRepository => _ratingsRepository ?? (_ratingsRepository = new RatingsRepository());
        public ICableReferenceMethodsRepository CableReferenceMethodsRepository => _cableReferenceMethodsRepository ?? (_cableReferenceMethodsRepository = new CableReferenceMethodsRepository());
        public ICertificateInspectionQuestionsRepository CertificateInspectionQuestionsRepository => _certificateInspectionQuestionsRepository ?? (_certificateInspectionQuestionsRepository = new CertificateInspectionQuestionsRepository());
        public IBsAmendmentDatesRepository BsAmendmentDatesRepository => _bsAmendmentDatesRepository ?? (_bsAmendmentDatesRepository = new BsAmendmentDatesRepository());
        public IRcdTypeRepository RcdTypeRepository => _rcdTypeRepository ?? (_rcdTypeRepository = new RcdTypeRepository());
        public IRcdOpCurrentsRepository RcdOpCurrentsRepository => _rcdOpCurrentsRepository ?? (_rcdOpCurrentsRepository = new RcdOpCurrentsRepository());
        public ICsaCpcRepository CsaCpcRepository => _csaCpcRepository ?? (_csaCpcRepository = new CsaCpcRepository());
        public ICsaLiveRepository CsaLiveRepository => _csaLiveRepository ?? (_csaLiveRepository = new CsaLiveRepository());
        public INamingConventionRepository NamingConventionRepository => _namingConventionRepository ?? (_namingConventionRepository = new NamingConventionRepository());

        public DatabaseManager()
        {
            _boardRepository = new BoardRepository();
            _boardRowRepository = new BoardRowRepository();
            _buildingRepository = new BuildingRepository();
            _buildingUserRepository = new BuildingUserRepository();
            _certificateRepository = new CertificateRepository();
            _certificateRowRepository = new CertificateRowRepository();
            _circuitRepository = new CircuitRepository();
            _estateRepository = new EstateRepository();
            _buildingRowRepository = new BuildingRowRepository();
            _userRepository = new UserRepository();
            _instrumentRepository = new InstrumentRepository();
            _certificateInspectionRepository = new CertificateInspectionRepository();
            _buildingContactTestRepository = new BuildingContactTestRepository();
            _buildingTestRepository = new BuildingTestRepository();
            _boardTestRepository = new BoardTestRepository();
            _circuitTestRepository = new CircuitTestRepository();
            _circuitPointsRcdTestRepository = new CircuitPointsRcdTestRepository();
            _certificateInspectionObservationsRepository = new CertificateInspectionObservationsRepository();
            _circuitPointsRcdRepository = new CircuitPointsRcdRepository();
            _observationFromRepository = new ObservationFromRepository();
            _observationGroupRepository = new ObservationGroupRepository();
            _boardReferenceTypesRepository = new BoardReferenceTypesRepository();
            _conductorSizeTypesRepository = new ConductorSizeTypesRepository();
            _circuitOptsRepository = new CircuitOptsRepository();
            _optsRepository = new OptsRepository();
            _phasesRepository = new PhasesRepository();
            _phaseSortOrdersRepository = new PhaseSortOrdersRepository();
            _ratingsRepository = new RatingsRepository();
            _cableReferenceMethodsRepository = new CableReferenceMethodsRepository();
            _certificateInspectionQuestionsRepository = new CertificateInspectionQuestionsRepository();
            _bsAmendmentDatesRepository = new BsAmendmentDatesRepository();
            _rcdTypeRepository = new RcdTypeRepository();
            _rcdOpCurrentsRepository = new RcdOpCurrentsRepository();
            _csaLiveRepository = new CsaLiveRepository();
            _csaCpcRepository = new CsaCpcRepository();
            _namingConventionRepository = new NamingConventionRepository();
        }
    }
}