using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Domain.Circuits;
using EDIS.Domain.EstatesLookups;
using EDIS.Domain.Lookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Points
{
    public class EditPointDetailsViewModel : BaseViewModel
    {
        private NavigationDataEndPoint _navigationData;

        private readonly IBoardsService _boardsService;
        private readonly IEstatesLookupsService _estatesLookups;
        private readonly ILookupsService _lookupsService;

        private bool _anyPoint;

        public bool AnyPoint
        {
            get { return _anyPoint; }
            set { Set(() => AnyPoint, ref _anyPoint, value); }
        }

        private int _selectedCircuitPolarity;

        public int SelectedCircuitPolarity
        {
            get
            {
                if (SelectedEndPoint != null)
                    return SelectedEndPoint.CircuitPointPolarity ?? 0;
                return 0;
            }
            set
            {
                if (SelectedEndPoint != null)
                    SelectedEndPoint.CircuitPointPolarity = value;
            }
        }

        private int _selectedCircuitRcdTestButton;

        public int SelectedCircuitRcdTestButton
        {
            get
            {
                if (SelectedEndPoint != null)
                    return SelectedEndPoint.CircuitRcdTestButton ?? 0;
                return 0;
            }
            set
            {
                if (SelectedEndPoint != null)
                    SelectedEndPoint.CircuitRcdTestButton = value;
            }
        }

        private List<ObservationFrom> _observationsFrom;

        public List<ObservationFrom> ObservationsFrom
        {
            get { return _observationsFrom; }
            set { Set(() => ObservationsFrom, ref _observationsFrom, value); }
        }

        private ObservableCollectionFast<CircuitPointsRcdTest> _endPoints;

        public ObservableCollectionFast<CircuitPointsRcdTest> EndPoints
        {
            get { return _endPoints; }
            set { Set(() => EndPoints, ref _endPoints, value); }
        }

        private List<PolarityValue> _polarityValues;

        public List<PolarityValue> PolarityValues
        {
            get { return _polarityValues; }
            set { Set(() => PolarityValues, ref _polarityValues, value); }
        }

        private CircuitPointsRcdTest _selectedEndPoint;

        public CircuitPointsRcdTest SelectedEndPoint
        {
            get { return _selectedEndPoint; }
            set
            {
                if (value == null)
                    return;
                Set(() => SelectedEndPoint, ref _selectedEndPoint, value);

                _selectedObservation = ObservationsFrom.FirstOrDefault(x => x.ObsFromId == SelectedEndPoint.ObsFromId);
                _selectedClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == SelectedEndPoint.DbcctClassificationCode);
                SelectedCircuitPolarity = PolarityValues.IndexOf(PolarityValues.FirstOrDefault(x => value.CircuitPointPolarity != null && x.Value == value.CircuitPointPolarity.Value));
                RaisePropertyChanged(() => SelectedCircuitPolarity);
                SelectedCircuitRcdTestButton = PolarityValues.IndexOf(PolarityValues.FirstOrDefault(x => value.CircuitRcdTestButton != null && x.Value == value.CircuitRcdTestButton.Value));
                RaisePropertyChanged(() => SelectedCircuitRcdTestButton);
                RaisePropertyChanged(() => SelectedObservation);
                RaisePropertyChanged(() => SelectedClassificationCode);
                _setClassification = SelectedObservation == null;

                RcdOpCurrent = value.CircuitRcdCurrent;
                RcdType = value.CircuitRcdBsenNum;
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
                SelectedEndPoint.CircuitRcdBsenNum = value;
            }
        }

        private string _rcdOpCurrent;

        public string RcdOpCurrent
        {
            get { return _rcdOpCurrent; }
            set
            {
                Set(() => RcdOpCurrent, ref _rcdOpCurrent, value);
                SelectedEndPoint.CircuitRcdCurrent = value;
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

        private RelayCommand _savePointCommand;

        public RelayCommand SavePointCommand
        {
            get
            {
                return _savePointCommand ?? (_savePointCommand = new RelayCommand(async () =>
                {
                    if (SelectedEndPoint == null)
                    {
                        return;
                    }

                    try
                    {
                        var isSaved = false;
                        
                        using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                        {
                            var circuitResult = await _boardsService.GetTestedCircuit(_navigationData.CircuitTestId);
                            if (!circuitResult.Success)
                            {
                                Dialogs.Toast(circuitResult.Message);
                                return;
                            }

                            var circuit = circuitResult.ResultObject as CircuitTest;

                            var measuredCheck = double.TryParse(SelectedEndPoint.CircuitPointMeasuredZs, out double measured);
                            var maxCheck = double.TryParse(circuit?.CircuitMaxPermittedImpedance, out double max);

                            if (measuredCheck && maxCheck)
                            {
                                if (measured >= max * 0.8)
                                {
                                    await _boardsService.SaveCircuitPoint(SelectedEndPoint, _navigationData.CircuitTestId, EstateId, true);
                                    isSaved = true;

                                    var response = await _estatesLookups.GetObservationFromLookups(EstateId);
                                    var observations = response.ToList();
                                    var defaultObs = observations.FirstOrDefault(x => x.DefaultObservation == 1);

                                    SelectedEndPoint.ObsFromId = defaultObs?.ObsFromId;
                                    SelectedEndPoint.DbcctClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == defaultObs?.ObsCatCode)?.Code;

                                    SelectedObservation = ObservationsFrom.FirstOrDefault(x => x.ObsFromId == SelectedEndPoint.ObsFromId);
                                }
                            }

                            if(!isSaved)
                                await _boardsService.SaveCircuitPoint(SelectedEndPoint, _navigationData.CircuitTestId, EstateId, false);

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

        private RelayCommand _deletePointCommand;

        public RelayCommand DeletePointCommand
        {
            get
            {
                return _deletePointCommand ?? (_deletePointCommand = new RelayCommand(async () =>
                {
                    try
                    {
                        await _boardsService.DeleteCircuitPoint(SelectedEndPoint.CprTestId);
                        EndPoints.Remove(SelectedEndPoint);
                        Dialogs.Toast("successful");
                    }
                    catch (Exception e)
                    {
                        await ShowErrorAlert(e.Message);
                    }
                }));
            }
        }

        private RelayCommand _addNewCommand;

        public RelayCommand AddNewCommand
        {
            get
            {
                return _addNewCommand ?? (_addNewCommand = new RelayCommand(() =>
                {
                    var newEndPoint = new CircuitPointsRcdTest
                    {
                        CprTestId = Guid.NewGuid().ToString(),
                        CertId = CertificateId,
                        BoardId = _navigationData.BoardId,
                        CircuitId = _navigationData.CircuitId,
                        CircuitReference = _navigationData.CircuitReference,
                        CircuitPhase = _navigationData.CircuitPhase
                    };
                    EndPoints.Add(newEndPoint);
                    SelectedEndPoint = EndPoints.Last();
                    AnyPoint = true;
                }));
            }
        }

        private RelayCommand<CircuitPointsRcdTest> _endPointSelectedCommand;

        public RelayCommand<CircuitPointsRcdTest> EndPointSelectedCommand
        {
            get
            {
                return _endPointSelectedCommand ?? (_endPointSelectedCommand = new RelayCommand<CircuitPointsRcdTest>((endPoint) =>
                {
                    if (endPoint == null)
                        return;


                    SelectedEndPoint = endPoint;
                }));
            }
        }

        private bool _setClassification;

        private List<ClassificationCode> _clasifficationCodes;

        public List<ClassificationCode> ClasifficationCodes
        {
            get { return _clasifficationCodes; }
            set { Set(() => ClasifficationCodes, ref _clasifficationCodes, value); }
        }

        private ClassificationCode _selectedClassificationCode;

        public ClassificationCode SelectedClassificationCode
        {
            get { return _selectedClassificationCode; }
            set
            {
                if (value == null) return;
                Set(() => SelectedClassificationCode, ref _selectedClassificationCode, value);
                SelectedEndPoint.DbcctClassificationCode = value.Code;
            }
        }

        private ObservationFrom _selectedObservation;

        public ObservationFrom SelectedObservation
        {
            get { return _selectedObservation; }
            set
            {
                if (value == null) return;
                Set(() => SelectedObservation, ref _selectedObservation, value);
                SelectedEndPoint.ObsFromId = value.ObsFromId;
                if(_setClassification)
                    SelectedClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == value.ObsCatCode );
                _setClassification = true;
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var data = navigationData as NavigationDataEndPoint;
            if (data != null)
                _navigationData = data;

            var rcdTypes = await _lookupsService.GetRcdTypes();
            RcdTypes = rcdTypes.ToList();

            var rcdOpCurents = await _lookupsService.GetRcdOpCurrents();
            RcdOpCurrents = rcdOpCurents.ToList();

            var observations = await _estatesLookups.GetObservationFromLookups(EstateId);
            ObservationsFrom = (List<ObservationFrom>)observations;

            var circuitEndPoints = await _boardsService.GetCircuitPoints(_navigationData.CircuitId);
            EndPoints.Clear();
            var endPoints = circuitEndPoints.ResultObject as IEnumerable<CircuitPointsRcdTest>;

            if (endPoints == null)
            {
                AnyPoint = false;
                return;
            }

            AnyPoint = true;
            EndPoints.AddRange(endPoints.Where(x => x.CircuitPhase == _navigationData.CircuitPhase));
            SelectedEndPoint = EndPoints.FirstOrDefault();
        }

        public EditPointDetailsViewModel(IBoardsService boardsService, IEstatesLookupsService estatesLookups, ILookupsService lookupsService)
        {
            Title = "Edit Point Details";

            _boardsService = boardsService;
            _estatesLookups = estatesLookups;
            _lookupsService = lookupsService;

            EndPoints = new ObservableCollectionFast<CircuitPointsRcdTest>();
            ObservationsFrom=new List<ObservationFrom>();
            
            //SelectedClassificationCode = new ClassificationCode();
            //SelectedObservation = new ObservationFrom();
            ClasifficationCodes = new List<ClassificationCode>
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

            PolarityValues = new List<PolarityValue>
            {
                new PolarityValue
                {
                    Value = 0,
                    Text = "Blank"
                },
                new PolarityValue
                {
                    Value = 1,
                    Text = "Tick"
                },
                new PolarityValue
                {
                    Value = 2,
                    Text = "Cross"
                },
                new PolarityValue
                {
                    Value = 3,
                    Text = "Blank"
                },
                new PolarityValue
                {
                    Value = 4,
                    Text = "Tick"
                },
                new PolarityValue
                {
                    Value = 5,
                    Text = "Cross"
                }
            };

            //SelectedEndPoint = new CircuitPointsRcdTest();
        }
    }
}