using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Boards;
using EDIS.Domain.Lookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Boards.BoardDetails
{
    public class BasicInfoViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;
        private readonly ILookupsService _lookupsService;

        private BoardTest _board;

        public BoardTest Board
        {
            get => _board;
            set
            {
                Set(() => Board, ref _board, value);
                if (value != null)
                {
                    RcdType = value.BoardRcd;
                    RcdOpCurrent = value.BoardRcdRating;
                }
            }
        }

        private List<ConductorSizeTypes> _conductorSizeTypes;

        public List<ConductorSizeTypes> ConductorSizeTypes
        {
            get { return _conductorSizeTypes; }
            set
            {
                Set(() => ConductorSizeTypes, ref _conductorSizeTypes, value);
                RaisePropertyChanged(() => SelectedConductorSizeType);
            }
        }

        private List<NamingConventions> _namingConventions;

        public List<NamingConventions> NamingConventions
        {
            get { return _namingConventions; }
            set
            {
                Set(() => NamingConventions, ref _namingConventions, value);
            }
        }

        private List<NamingConventions> _allNamingConventions;

        public List<NamingConventions> AllNamingConventions
        {
            get { return _allNamingConventions; }
            set
            {
                Set(() => AllNamingConventions, ref _allNamingConventions, value);
            }
        }

        private List<BoardReferenceTypes> _boardReferenceTypes;

        public List<BoardReferenceTypes> BoardReferenceTypes
        {
            get { return _boardReferenceTypes; }
            set
            {
                Set(() => BoardReferenceTypes, ref _boardReferenceTypes, value);
                RaisePropertyChanged(() => SelectedBoardReferenceType);
            }
        }

        private List<Phases> _phases;

        public List<Phases> Phases
        {
            get { return _phases; }
            set
            {
                Set(() => Phases, ref _phases, value);
            }
        }

        private List<RcdTypes> _rcdTypes;

        public List<RcdTypes> RcdTypes
        {
            get { return _rcdTypes; }
            set { Set(() => RcdTypes, ref _rcdTypes, value); }
        }

        private List<RcdOpCurrents> _rcdOpCurrents;

        public List<RcdOpCurrents> RcdOpCurrents
        {
            get { return _rcdOpCurrents; }
            set { Set(() => RcdOpCurrents, ref _rcdOpCurrents, value); }
        }

        private string _rcdType;

        public string RcdType
        {
            get { return _rcdType; }
            set
            {
                Set(() => RcdType, ref _rcdType, value);
                Board.BoardRcd = value;
            }
        }

        private string _rcdOpCurrent;

        public string RcdOpCurrent
        {
            get { return _rcdOpCurrent; }
            set
            {
                Set(() => RcdOpCurrent, ref _rcdOpCurrent, value);
                Board.BoardRcdRating = value;
            }
        }

        private RcdTypes _selectedRcdType;

        public RcdTypes SelectedRcdType
        {
            get => null;
            set
            {
                if (value != null)
                {
                    RcdType = value.RcdType;
                }
            }
        }

        private RcdOpCurrents _selectedRcdOpCurrent;

        public RcdOpCurrents SelectedRcdOpCurrent
        {
            get => null;
            set
            {
                if (value != null)
                {
                    RcdOpCurrent = value.RcdOpCurrent;
                }
            }
        }

        private ConductorSizeTypes _selectedConductorSizeType;

        public ConductorSizeTypes SelectedConductorSizeType
        {
            get
            {
                if (Board != null && ConductorSizeTypes != null)
                    return ConductorSizeTypes.FirstOrDefault(x => x.ConductorSizeType == Board.BoardSupplyCircuitConductorSizeType);
                return null;
            }
            set
            {
                IsOther = value?.ConductorSizeType == "O";
                if (!IsOther)
                {
                    Board.BoardSupplyCircuitConductorSizeTypeOther = "";
                    RaisePropertyChanged(() => Board);
                }
                if (Board != null && ConductorSizeTypes != null)
                    Board.BoardSupplyCircuitConductorSizeType = value?.ConductorSizeType;
            }
        }

        private Phases _selectedPhase;

        public Phases SelectedPhase
        {
            get { return _selectedPhase; }
            set
            {
                if (Board != null && ConductorSizeTypes != null)
                {
                    Set(() => SelectedPhase, ref _selectedPhase, value);

                    switch (value?.PhaseValue)
                    {
                        case "1": case "2PN": case "S": case "SP&N":
                        {
                                NamingConventions = AllNamingConventions.Where(x => x.NamingConvention.Equals("L1") ||
                                                                                    x.NamingConvention.Equals("L2") ||
                                                                                    x.NamingConvention.Equals("L3") ||
                                                                                    x.NamingConvention.Equals("R") ||
                                                                                    x.NamingConvention.Equals("Y") ||
                                                                                    x.NamingConvention.Equals("B") ||
                                                                                    x.NamingConvention.Equals("S")).ToList();
                            break;
                        }
                        case "3": case "TP&N":
                        {
                            NamingConventions = AllNamingConventions.Where(x => x.NamingConvention.Equals("L1L2L3") ||
                                                                                x.NamingConvention.Equals("RYB")).ToList();
                                break;
                        }
                    }
                }
            }
        }

        private BoardReferenceTypes _selectedBoardReferenceType;

        public BoardReferenceTypes SelectedBoardReferenceType
        {
            get { return _selectedBoardReferenceType; }
            set { Set(() => SelectedBoardReferenceType, ref _selectedBoardReferenceType, value); }
        }

        private NamingConventions _selectedNamingConventions;

        public NamingConventions SelectedNamingConvention
        {
            get { return _selectedNamingConventions; }
            set { Set(() => SelectedNamingConvention, ref _selectedNamingConventions, value); }
        }

        private bool _isOther;

        public bool IsOther
        {
            get { return _isOther; }
            set { Set(() => IsOther, ref _isOther, value); }
        }

        private List<CircuitOpts> _circuitOpts;

        public List<CircuitOpts> CircuitOpts
        {
            get { return _circuitOpts; }
            set { Set(() => CircuitOpts, ref _circuitOpts, value); }
        }

        private List<BsenType> _bsenTypes;

        public List<BsenType> BsenTypes
        {
            get { return _bsenTypes; }
            set
            {
                Set(() => BsenTypes, ref _bsenTypes, value);
            }
        }

        private BsenType _selectedBsenType;

        public BsenType SelectedBsenType
        {
            get { return _selectedBsenType; }
            set
            {
                //if (value == null) return;

                Set(() => SelectedBsenType, ref _selectedBsenType, value);

                var opts = CircuitOpts.Where(x => x.CircuitOptBsen == value?.Bsen &&
                                                  x.CircuitOptType == value?.Type).ToList();

                Ratings = opts.Select(x => x.CircuitOptRating).Distinct().ToList();
            }
        }

        private string _bsenSearch;

        public string BsenSearch
        {
            get { return _bsenSearch; }
            set
            {
                Set(() => BsenSearch, ref _bsenSearch, value);
            }
        }

        private string _ratingSearch;

        public string RatingSearch
        {
            get { return _ratingSearch; }
            set
            {
                Set(() => RatingSearch, ref _ratingSearch, value);
            }
        }

        private RelayCommand _bsenSearchChanged;

        public RelayCommand BsenSearchChanged
        {
            get
            {
                return _bsenSearchChanged ?? (_bsenSearchChanged = new RelayCommand(() =>
                {
                    var searched = CircuitOpts.Where(x => x.CircuitOptBsen.Contains(BsenSearch)).ToList();
                    BsenTypes = searched.Select(x => new BsenType { Bsen = x.CircuitOptBsen, Type = x.CircuitOptType }).Distinct().ToList();
                }));
            }
        }

        private RelayCommand _ratingSearchChanged;

        public RelayCommand RatingSearchChanged
        {
            get
            {
                return _ratingSearchChanged ?? (_ratingSearchChanged = new RelayCommand(() =>
                {
                    var searched = CircuitOpts.Where(x => x.CircuitOptRating.Contains(RatingSearch));
                    Ratings = searched.Select(x => x.CircuitOptRating).Distinct().ToList();
                }));
            }
        }

        private List<string> _ratings;

        public List<string> Ratings
        {
            get { return _ratings; }
            set
            {
                Set(() => Ratings, ref _ratings, value);
            }
        }

        private string _selectedRating;

        public string SelectedRating
        {
            get { return _selectedRating; }
            set
            {
                //if (value == null) return;

                Set(() => SelectedRating, ref _selectedRating, value);
            }
        }

        private RelayCommand _upload;

        public RelayCommand Upload
        {
            get
            {
                return _upload ?? (_upload = new RelayCommand(async () =>
                {
                    try
                    {
                        var result = await _boardsService.UploadBoard(Board.BoardId);
                        Dialogs.Toast(result.Success ? "Success" : result.Message);
                    }
                    catch (ServiceAuthenticationException e)
                    {
                        var result = await TryToLogin();
                        if (!result)
                            await NavigationService.NavigateToAsync<LoginViewModel>();
                        else
                            Upload.Execute(null);
                    }
                    catch (Exception e)
                    {
                        await ShowErrorAlert(e.Message);
                    }
                }));
            }
        }

        private RelayCommand _save;

        public RelayCommand Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand(async () =>
                {
                    try
                    {
                        Board.BoardReferenceType = SelectedBoardReferenceType?.BoardReferenceType;
                        Board.BoardOpt = SelectedBsenType?.Bsen;
                        Board.BoardOptType = SelectedBsenType?.Type;
                        Board.BoardSupplyConductorRating = SelectedRating;

                        Board.BoardPhase = SelectedPhase?.PhaseValue;
                        Board.BoardCircuitPhaseNaming = SelectedNamingConvention?.NamingConvention;

                        await _boardsService.SaveBoardTest(Board);

                        Dialogs.Toast("Successful");
                    }
                    catch (Exception e)
                    {
                        await ShowErrorAlert(e?.Message);
                    }
                }));
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var types = await _lookupsService.GetConductorSizeTypes(false);
                ConductorSizeTypes = types.ToList();

                var namingConventions = await _lookupsService.GetNamingConventions();
                AllNamingConventions = namingConventions.ToList();

                var phases = await _lookupsService.GetPhases();
                Phases = phases.ToList();
                
                var boardReftypes = await _lookupsService.GetBoardReferenceTypes();
                BoardReferenceTypes = boardReftypes.ToList();

                var rcdTypes = await _lookupsService.GetRcdTypes();
                RcdTypes = rcdTypes.ToList();

                var rcdOpCurrents = await _lookupsService.GetRcdOpCurrents();
                RcdOpCurrents = rcdOpCurrents.ToList();

                var circuitOpts = await _lookupsService.GetCircuitOpts();
                CircuitOpts = circuitOpts.Where(x => x.CircuitOptInactive == "N").ToList();

                BsenTypes = CircuitOpts.Select(x => new BsenType { Bsen = x.CircuitOptBsen, Type = x.CircuitOptType }).Distinct().ToList();
                Ratings = CircuitOpts.Select(x => x.CircuitOptRating).Distinct().ToList();

                SelectedBsenType = BsenTypes.FirstOrDefault(x => x.Bsen == Board.BoardOpt && x.Type == Board.BoardOptType);
                SelectedRating = Board.BoardSupplyConductorRating;

                SelectedPhase = Phases.FirstOrDefault(x => x.PhaseValue == Board.BoardPhase);
                SelectedNamingConvention = AllNamingConventions.FirstOrDefault(x => x.NamingConvention == Board.BoardCircuitPhaseNaming);

                SelectedBoardReferenceType = BoardReferenceTypes.FirstOrDefault(x => x.BoardReferenceType == Board.BoardReferenceType);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e?.Message);
            }

        }

        public BasicInfoViewModel(IBoardsService boardsService, ILookupsService lookupsService)
        {
            Title = "Basic Info";

            _boardsService = boardsService;
            _lookupsService = lookupsService;
            Board = new BoardTest();

            CircuitOpts = new List<CircuitOpts>();
            Ratings = new List<string>();
            BsenTypes = new List<BsenType>();
        }
    }

    public class BsenType
    {
        public string Bsen { get; set; }
        public string Type { get; set; }

        public string Representation => Bsen + " " + Type;

        public override bool Equals(object obj)
        {
            try
            {
                var item = obj as BsenType;

                if (item == null)
                    return false;

                if (Type != null && item.Type != null)
                    return Bsen.Equals(item.Bsen) && Type.Equals(item.Type);

                return Type == null && item.Type == null;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Bsen.GetHashCode();
        }
    }
}