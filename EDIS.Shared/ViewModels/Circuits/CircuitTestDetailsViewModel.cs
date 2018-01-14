using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Domain.Circuits;
using EDIS.Domain.EstatesLookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Circuits
{
    public class CircuitTestDetailsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;
        private readonly IEstatesLookupsService _estatesLookups;

        private string _r1r2;

        public string R1R2
        {
            get { return Circuit.CircuitImpedanceR1R2; }
            set
            {
                Set(() => R1R2, ref _r1r2, value);
                Circuit.CircuitImpedanceR1R2 = value;
            }
        }

        private string _r2;

        public string R2
        {
            get { return Circuit.CircuitImpedanceR2; }
            set
            {
                Set(() => R2, ref _r2, value);
                Circuit.CircuitImpedanceR2 = value;
            }
        }

        private int _selectedCircuitPolarity;

        public int SelectedCircuitPolarity
        {
            get => Circuit.CircuitPolarityCorrect ?? 0;
            set => Circuit.CircuitPolarityCorrect = value;
        }

        private int _selectedRcdTestButton;

        public int SelectedRcdTestButton
        {
            get => Circuit.CircuitRcdtestButton ?? 0;
            set => Circuit.CircuitRcdtestButton = value;
        }

        private List<ObservationFrom> _observationsFrom;

        public List<ObservationFrom> ObservationsFrom 
        {
            get { return _observationsFrom; }
            set { Set(() => ObservationsFrom, ref _observationsFrom, value); }
        }

        private List<PolarityValue> _polarityValues;

        public List<PolarityValue> PolarityValues
        {
            get { return _polarityValues; }
            set { Set(() => PolarityValues, ref _polarityValues, value); }
        }

        private string _phasephase;

        public string PhasePhase
        {
            get { return _phasephase; }
            set
            {
                Set(() => PhasePhase, ref _phasephase, value);
                Circuit.CircuitInsulationResistancePhasePhase = value;
            }
        }

        private string _phaseearth;

        public string PhaseEarth
        {
            get { return _phaseearth; }
            set
            {
                Set(() => PhaseEarth, ref _phaseearth, value);
                Circuit.CircuitInsulationResistancePhaseEarth = value;
            }
        }

        private Resistance _selectePhasePhase;

        public Resistance SelectedPhasePhase
        {
            get => null;
            set
            {
                if (value != null)
                {
                    PhasePhase = value.Text;
                }
            }
        }

        private Resistance _selectedPhaseEarth;

        public Resistance SelectedPhaseEarth
        {
            get => null;
            set
            {
                if (value != null)
                {
                    PhaseEarth = value.Text;
                }
            }
        }

        private List<Resistance> _resistanceValues;

        public List<Resistance> ResistancesValues
        {
            get { return _resistanceValues; }
            set { Set(() => ResistancesValues, ref _resistanceValues, value); }
        }

        private List<RcdTestButtonValue> _rcdTestButtons;

        public List<RcdTestButtonValue> RcdTestButtons
        {
            get { return _rcdTestButtons; }
            set { Set(() => RcdTestButtons, ref _rcdTestButtons, value); }
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

                                    SelectedObservation = ObservationsFrom.FirstOrDefault(x => x.ObsFromId == Circuit.ObsFromId);
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

        private void RefreshObservation()
        {
            MessagingCenter.Subscribe<CircuitDetailsViewModel>(this, MessengerCenterMessages.RefreshObservation,
                model =>
                {
                    _selectedObservation = ObservationsFrom.FirstOrDefault(x => x.ObsFromId == Circuit.ObsFromId);
                    RaisePropertyChanged(() => SelectedObservation);
                });
        }

        private CircuitTest _circuit;

        public CircuitTest Circuit
        {
            get => _circuit;
            set
            {
                Set(() => Circuit, ref _circuit, value); 
                RaisePropertyChanged(() => R1R2);
                RaisePropertyChanged(() => R2);
                PhaseEarth = value.CircuitInsulationResistancePhaseEarth;
                PhasePhase = value.CircuitInsulationResistancePhasePhase;
                SelectedCircuitPolarity = PolarityValues.IndexOf(PolarityValues.FirstOrDefault(x => value.CircuitPolarityCorrect != null && x.Value == value.CircuitPolarityCorrect.Value));
                SelectedRcdTestButton = RcdTestButtons.IndexOf(RcdTestButtons.FirstOrDefault(x => value.CircuitRcdtestButton != null && x.Value == value.CircuitRcdtestButton.Value));
                RaisePropertyChanged(() => SelectedCircuitPolarity);
                RaisePropertyChanged(() => SelectedRcdTestButton);
            }
        }

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
                Set(() => SelectedClassificationCode, ref _selectedClassificationCode, value);
                Circuit.DbcctClassificationCode = value.Code;
            }
        }

        private ObservationFrom _selectedObservation;

        public ObservationFrom SelectedObservation
        {
            get { return _selectedObservation; }
            set
            {
                Set(() => SelectedObservation, ref _selectedObservation, value);
                Circuit.ObsFromId = value.ObsFromId;
                if (_setClassification)
                    SelectedClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == value.ObsCatCode);
                _setClassification = true;
            }
        }

        private bool _setClassification;

        public override async Task InitializeAsync(object navigationData)
        {
            var observations = await _estatesLookups.GetObservationFromLookups(EstateId);
            ObservationsFrom = (List<ObservationFrom>) observations;
            _selectedObservation = ObservationsFrom.FirstOrDefault(x => x.ObsFromId == Circuit.ObsFromId);
            _selectedClassificationCode = ClasifficationCodes.FirstOrDefault(x => x.Code == Circuit.DbcctClassificationCode);
            RaisePropertyChanged(() => SelectedObservation);
            RaisePropertyChanged(() => SelectedClassificationCode);
            _setClassification = SelectedObservation == null;

            RefreshObservation();
        }

        public CircuitTestDetailsViewModel(IBoardsService boardsService, IEstatesLookupsService estatesLookups)
        {
            Title = "Circuit Test Details";

            _boardsService = boardsService;
            _estatesLookups = estatesLookups;
            
            ObservationsFrom = new List<ObservationFrom>();

            PolarityValues = new List<PolarityValue>
            {
                new PolarityValue
                {
                    Value = 0,
                    Text = ""
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
                    Text = ""
                },
                new PolarityValue
                {
                    Value = 4,
                    Text = "Tick"
                },
            };

            ResistancesValues = new List<Resistance>
            {
                new Resistance
                {
                    Value = 0,
                    Text = ">499"
                },
                new Resistance
                {
                    Value = 1,
                    Text = ">399"
                },
                new Resistance
                {
                    Value = 2,
                    Text = ">299"
                }
            };

            RcdTestButtons = new List<RcdTestButtonValue>
            {
                new RcdTestButtonValue
                {
                    Value = 0,
                    Text = "Blank"
                },
                new RcdTestButtonValue
                {
                    Value = 1,
                    Text = "Tick"
                },
                new RcdTestButtonValue
                {
                    Value = 2,
                    Text = "Cross"
                },
                new RcdTestButtonValue
                {
                    Value = 3,
                    Text = "Tick"
                },
                new RcdTestButtonValue
                {
                    Value = 4,
                    Text = "Cross"
                }
            };

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

            Circuit = new CircuitTest();
        }
    }
}