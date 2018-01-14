using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using EDIS.Domain.Lookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ObjectBuilder2;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Circuits
{
    public class CircuitDetailsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;
        private readonly ILookupsService _lookupsService;
        private readonly IEstatesLookupsService _estatesLookups;

        private CircuitTest _circuit;

        public CircuitTest Circuit
        {
            get => _circuit;
            set
            {
                Set(() => Circuit, ref _circuit, value);
                if (value != null)
                {
                    RcdOpCurrent = value.CircuitRcdOpCurrent;

                    CsaCpc = value.CircuitConductorCsaCpc;
                    CsaLive = value.CircuitConductorCsaLive;
                }
            }
        }

        private string _boardPhase;

        public string BoardPhase
        {
            get { return _boardPhase; }
            set { Set(() => BoardPhase, ref _boardPhase, value); }
        }

        private string _namingConvention;

        public string NamingConvention
        {
            get { return _namingConvention; }
            set { Set(() => NamingConvention, ref _namingConvention, value); }
        }

        private List<CsaCpc> _csaCpcs;

        public List<CsaCpc> CsaCpcs
        {
            get { return _csaCpcs; }
            set { Set(() => CsaCpcs, ref _csaCpcs, value); }
        }

        private List<CsaLive> _csaLives;

        public List<CsaLive> CsaLives
        {
            get { return _csaLives; }
            set { Set(() => CsaLives, ref _csaLives, value); }
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

        private List<CableReferenceMethods> _cableReferenceMethods;

        public List<CableReferenceMethods> CableReferenceMethods
        {
            get { return _cableReferenceMethods; }
            set
            {
                Set(() => CableReferenceMethods, ref _cableReferenceMethods, value);
                RaisePropertyChanged(() => SelectedCableReferenceMethod);
            }
        }

        private List<string> _circuitPhases;

        public List<string> CircuitPhases
        {
            get { return _circuitPhases; }
            set
            {
                Set(() => CircuitPhases, ref _circuitPhases, value);
            }
        }

        private List<RcdOpCurrents> _rcdOpCurrents;

        public List<RcdOpCurrents> RcdOpCurrents
        {
            get { return _rcdOpCurrents; }
            set { Set(() => RcdOpCurrents, ref _rcdOpCurrents, value); }
        }

        private List<CircuitOpts> _circuitOpts;

        public List<CircuitOpts> CircuitOpts
        {
            get { return _circuitOpts; }
            set { Set(() => CircuitOpts, ref _circuitOpts, value); }
        }

        private List<RcdTypes> _rcdTypes;

        public List<RcdTypes> RcdTypes
        {
            get { return _rcdTypes; }
            set { Set(() => RcdTypes, ref _rcdTypes, value); }
        }

        private string _rcdType;

        public string RcdType
        {
            get { return _rcdType; }
            set
            {
                Set(() => RcdType, ref _rcdType, value);
                Circuit.CircuitRcdBsen = value;
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

        private string _rcdOpCurrent;

        public string RcdOpCurrent
        {
            get { return _rcdOpCurrent; }
            set
            {
                Set(() => RcdOpCurrent, ref _rcdOpCurrent, value);
                Circuit.CircuitRcdOpCurrent = value;
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

        private CsaCpc _selectedCsaCpc;

        public CsaCpc SelectedCsaCpc
        {
            get => null;
            set
            {
                if (value != null)
                {
                    CsaCpc = value.CsaCpcValue;
                }
            }
        }

        private string _csaCpc;

        public string CsaCpc
        {
            get { return _csaCpc; }
            set
            {
                Set(() => CsaCpc, ref _csaCpc, value);
                Circuit.CircuitConductorCsaCpc = value;
            }
        }

        private CsaLive _selectedCsaLive;

        public CsaLive SelectedCsaLive
        {
            get => null;
            set
            {
                if (value != null)
                {
                    CsaLive = value.CsaLiveValue;
                }
            }
        }

        private string _csaLive;

        public string CsaLive
        {
            get { return _csaLive; }
            set
            {
                Set(() => CsaLive, ref _csaLive, value);
                Circuit.CircuitConductorCsaLive = value;
            }
        }

        private CableReferenceMethods _selectedCableReferenceMethod;

        public CableReferenceMethods SelectedCableReferenceMethod
        {
            get
            {
                return null;
            }
            set
            {
                if (Circuit != null && CableReferenceMethods != null && value != null)
                {
                    Circuit.CircuitReferenceMethodId = value.RefMethodId;
                    Circuit.CircuitReferenceMethod = value.RefMethodNumber;
                    RaisePropertyChanged(() => Circuit);
                }
            }
        }

        private ConductorSizeTypes _selectedConductorSizeType;

        public ConductorSizeTypes SelectedConductorSizeType
        {
            get
            {
                if (Circuit != null && ConductorSizeTypes != null)
                    return ConductorSizeTypes.FirstOrDefault(x => x.ConductorSizeType == Circuit.CircuitConductorSizeType);
                return null;
            }
            set
            {
                IsOther = value.ConductorSizeType == "O";
                if (!IsOther)
                {
                    Circuit.CircuitCableOtherText = "";
                    RaisePropertyChanged(() => Circuit);
                }
                if (Circuit != null && ConductorSizeTypes != null)
                    Circuit.CircuitConductorSizeType = value.ConductorSizeType;
            }
        }

        private string _selectedCircuitPhase;

        public string SelectedCircuitPhase
        {
            get { return _selectedCircuitPhase; }
            set
            {
                if (value == null) return;
                Set(() => SelectedCircuitPhase, ref _selectedCircuitPhase, value);
            }
        }

        private bool _isOther;

        public bool IsOther
        {
            get { return _isOther; }
            set { Set(() => IsOther, ref _isOther, value); }
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
                        var isSaved = false;

                        using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                        {
                            Circuit.CircuitMaxPermittedDisconnectionTime = SelectedDisconnectionTime;
                            Circuit.CircuitOcpd = SelectedBsenType?.Bsen;
                            Circuit.CircuitOcpdType = SelectedBsenType?.Type;
                            Circuit.CircuitOcpdRating = SelectedRating;
                            Circuit.CircuitMaxPermittedImpedance = SelectedMaxZs;

                            if (SelectedCircuitPhase != "L1L2L3" && SelectedCircuitPhase != "RYB")
                                Circuit.CircuitPhase = SelectedCircuitPhase;

                            var measuredCheck = double.TryParse(Circuit.CircuitMaxEarthLoop, out double measured);
                            var maxCheck = double.TryParse(Circuit.CircuitMaxPermittedImpedance, out double max);

                            if (measuredCheck && maxCheck)
                            {
                                if (measured >= max * 0.8)
                                {
                                    await _boardsService.SaveCircuitTest(Circuit, EstateId, true);
                                    isSaved = true;

                                    var response = await _estatesLookups.GetObservationFromLookups(EstateId);
                                    var observations = response.ToList();
                                    var defaultObs = observations.FirstOrDefault(x => x.DefaultObservation == 1);

                                    Circuit.ObsFromId = defaultObs?.ObsFromId;
                                    Circuit.DbcctClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == defaultObs?.ObsCatCode)?.Code;

                                    MessagingCenter.Send(this, MessengerCenterMessages.RefreshObservation);
                                }
                            }

                            var result = await _boardsService.GetTestedCircuits(CertificateId, Circuit.BoardId);
                            if (!result.Success)
                            {
                                Dialogs.Toast(result.Message);
                                return;
                            }

                            var circuitTest = result.ResultObject as List<CircuitTest>;
                            if (circuitTest == null)
                                return;

                            foreach (var circ in circuitTest)
                            {
                                if (circ.CircuitReference == Circuit.CircuitReference)
                                {
                                    circ.CircuitIs3Phase = Circuit.CircuitIs3Phase;
                                    await _boardsService.SaveCircuitTestWithoutObservation(circ);
                                }
                            }
                            
                            if(!isSaved)
                                await _boardsService.SaveCircuitTest(Circuit, EstateId, false);

                            Dialogs.Toast("Successful");
                        }
                    }
                    catch (Exception e)
                    {
                        await ShowErrorAlert(e?.Message);
                    }
                }));
            }
        }

        private bool _isInit = true;

        private List<string> _disconnectionTimes;

        public List<string> DisconnectionTimes
        {
            get { return _disconnectionTimes; }
            set
            {
                Set(() => DisconnectionTimes, ref _disconnectionTimes, value);
            }
        }

        private string _selectedDisconnectionTime;

        public string SelectedDisconnectionTime
        {
            get { return _selectedDisconnectionTime; }
            set
            {
                //if (value == null) return;

                Set(() => SelectedDisconnectionTime, ref _selectedDisconnectionTime, value);

                var opts = CircuitOpts.Where(x => x.CircuitOptMaxPermittedDisconnectionTime == value).ToList();

                BsenTypes = opts.Select(x => new BsenType { Bsen = x.CircuitOptBsen, Type = x.CircuitOptType }).Distinct().ToList();
                Ratings = opts.Select(x => x.CircuitOptRating).Distinct().ToList();
            }
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
                                                  x.CircuitOptMaxPermittedDisconnectionTime == SelectedDisconnectionTime &&
                                                  x.CircuitOptType == value?.Type).ToList();

                Ratings = opts.Select(x => x.CircuitOptRating).Distinct().ToList();
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

                var opts = CircuitOpts.Where(x => x.CircuitOptMaxPermittedDisconnectionTime == SelectedDisconnectionTime &&
                                                  x.CircuitOptBsen == SelectedBsenType?.Bsen &&
                                                  x.CircuitOptType == SelectedBsenType?.Type &&
                                                  x.CircuitOptRating == SelectedRating).ToList();
                SelectedMaxZs = opts.FirstOrDefault()?.CircuitOptMaxPermittedImpedance;
            }
        }

        private string _selectedMaxZs;

        public string SelectedMaxZs
        {
            get { return _selectedMaxZs; }
            set
            {
                Set(() => SelectedMaxZs, ref _selectedMaxZs, value);
                Circuit.CircuitMaxPermittedImpedance = SelectedMaxZs;
            }
        }

        private bool _isThreePhaseBoard;

        public bool IsThreePhaseBoard
        {
            get { return _isThreePhaseBoard; }
            set { Set(() => IsThreePhaseBoard, ref _isThreePhaseBoard, value); }
        }

        private string _circuitIs3Phase;

        public string CircuitIs3Phase
        {
            get { return _circuitIs3Phase; }
            set
            {
                Set(() => CircuitIs3Phase, ref _circuitIs3Phase, value);
                Circuit.CircuitIs3Phase = value;
                CheckThreePhaseCircuitPhases();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var types = await _lookupsService.GetConductorSizeTypes(false);
                ConductorSizeTypes = types.ToList();

                var typesRefMethod = await _lookupsService.GetCableReferenceMethods();
                CableReferenceMethods = typesRefMethod.ToList();

                //var namingConventions = await _lookupsService.GetNamingConventions();
                //NamingConventions = namingConventions.ToList();

                var rcdTypes = await _lookupsService.GetRcdTypes();
                RcdTypes = rcdTypes.ToList();

                var rcdOpCurents = await _lookupsService.GetRcdOpCurrents();
                RcdOpCurrents = rcdOpCurents.ToList();

                var csaCpcs = await _lookupsService.GetCsaCpcs();
                CsaCpcs = new List<CsaCpc>(csaCpcs.ToList().OrderBy(x => x.CsaCpcValue, new SemiNumericComparer()));

                var csaLives = await _lookupsService.GetCsaLives();
                CsaLives = new List<CsaLive>(csaLives.ToList().OrderBy(x => x.CsaLiveValue, new SemiNumericComparer()));

                var circuitOpts = await _lookupsService.GetCircuitOpts();
                CircuitOpts = circuitOpts.Where(x => x.CircuitOptInactive == "N").ToList();

                DisconnectionTimes = CircuitOpts.Select(x => x.CircuitOptMaxPermittedDisconnectionTime).Distinct()
                    .ToList();

                SelectedDisconnectionTime = Circuit.CircuitMaxPermittedDisconnectionTime;
                SelectedBsenType =
                    BsenTypes.FirstOrDefault(x => x.Bsen == Circuit.CircuitOcpd && x.Type == Circuit.CircuitOcpdType);
                SelectedRating = Circuit.CircuitOcpdRating;
                SelectedMaxZs = Circuit.CircuitMaxPermittedImpedance;

                var boardTestResult = await _boardsService.GetBoardTestDetails(Circuit.BoardId, CertificateId);
                if (!boardTestResult.Success)
                {
                    Dialogs.Toast(boardTestResult.Message);
                    return;
                }

                var boardTest = boardTestResult.ResultObject as BoardTest;
                BoardPhase = boardTest?.BoardPhase;
                NamingConvention = boardTest?.BoardCircuitPhaseNaming;

                if (string.IsNullOrEmpty(_selectedDisconnectionTime))
                {
                    SelectedDisconnectionTime = DisconnectionTimes.FirstOrDefault();
                }

                if (BoardPhase == "3" || BoardPhase =="TP&N")
                {
                    IsThreePhaseBoard = true;
                }
            }
            catch (Exception e)
            {

            }

            if (string.IsNullOrEmpty(NamingConvention))
                return;

            switch (BoardPhase)
            {
                case "1":
                case "2PN":
                case "S":
                case "SP&N":
                    {
                        CircuitPhases = new List<string>{NamingConvention};
                        SelectedCircuitPhase = NamingConvention;
                        break;
                    }
                case "3":
                case "TP&N":
                {
                    CircuitIs3Phase = Circuit.CircuitIs3Phase;
                    break;
                }
            }

            //SelectedNamingConventions = NamingConventions.FirstOrDefault(x => x.NamingConvention == Circuit.CircuitPhase);
        }

        private void CheckThreePhaseCircuitPhases()
        {
            switch (NamingConvention)
            {
                case "L1L2L3":
                    switch (Circuit.CircuitIs3Phase)
                    {
                        case "Y":
                            CircuitPhases = new List<string> { "L1L2L3" };
                            SelectedCircuitPhase = CircuitPhases.FirstOrDefault();
                            break;
                        case "N":
                            CircuitPhases = new List<string> { "L1", "L2", "L3" };
                            if (!string.IsNullOrEmpty(Circuit.CircuitPhase))
                            {
                                SelectedCircuitPhase = CircuitPhases.FirstOrDefault(x => x == Circuit.CircuitPhase);
                            }
                            break;
                    }
                    break;
                case "RYB":
                    switch (Circuit.CircuitIs3Phase)
                    {
                        case "Y":
                            CircuitPhases = new List<string> { "RYB" };
                            SelectedCircuitPhase = CircuitPhases.FirstOrDefault();
                            break;
                        case "N":
                            CircuitPhases = new List<string> { "R", "Y", "B" };
                            if (!string.IsNullOrEmpty(Circuit.CircuitPhase))
                            {
                                SelectedCircuitPhase = Circuit.CircuitPhase;
                            }
                            break;
                    }
                    break;
            }
        }

        public CircuitDetailsViewModel(IBoardsService boardsService, ILookupsService lookupsService, IEstatesLookupsService estatesLookups)
        {
            Title = "Circuit Details";

            _boardsService = boardsService;
            _lookupsService = lookupsService;
            _estatesLookups = estatesLookups;
            Circuit = new CircuitTest();

            CircuitOpts = new List<CircuitOpts>();
            Ratings = new List<string>();
            BsenTypes = new List<BsenType>();
        }

        List<ClassificationCode> ClasifficationCodes = new List<ClassificationCode>
        {
            new ClassificationCode
            {
                Code = "C1",
                Text = "C1 - Danger present: Risk of injury immediate remedial action required"
            },
            new ClassificationCode
            {
                Code = "C2",
                Text = "C2 - Potentially dangerous: Urgent remedial action required"
            },
            new ClassificationCode
            {
                Code = "C3",
                Text = "C3 - Improvement Recommended"
            },
            new ClassificationCode
            {
                Code = "NCFF",
                Text = "NCFF - Non-conformance found and attended to"
            },
            new ClassificationCode
            {
                Code = "FI",
                Text = "FI - Further investigation'"
            },
            new ClassificationCode
            {
                Code = "FIO",
                Text = "FIO - for information only"
            },
            new ClassificationCode
            {
                Code = "N/V",
                Text = "N/V - Not Verified"
            },
            new ClassificationCode
            {
                Code = "LIM",
                Text = "LIM - Limitation"
            },
            new ClassificationCode
            {
                Code = "N/A",
                Text = "N/A - Not Applicable"
            }
        };
    }

    public class BsenType
    {
        public string Bsen { get; set; }
        public string Type { get; set; }

        public string Representation => Bsen + " " + Type;

        public override bool Equals(object obj)
        {
            var item = obj as BsenType;

            if (item == null)
                return false;

            if (Type != null && item.Type != null)
                return Bsen.Equals(item.Bsen) && Type.Equals(item.Type);

            return Type == null && item.Type == null;
        }

        public override int GetHashCode()
        {
            return this.Bsen.GetHashCode();
        }
    }
}
