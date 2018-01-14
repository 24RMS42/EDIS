using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards.BoardData;
using EDIS.Shared.ViewModels.Circuits;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Points
{
    public class EditPointViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;

        private ObservableCollectionFast<BoardTest> _boards;

        public ObservableCollectionFast<BoardTest> Boards
        {
            get { return _boards; }
            set { Set(() => Boards, ref _boards, value); }
        }

        private ObservableCollectionFast<CircuitTest> _circuits;

        public ObservableCollectionFast<CircuitTest> Circuits
        {
            get { return _circuits; }
            set { Set(() => Circuits, ref _circuits, value); }
        }

        private int _currentBoardPosition;

        public int CurrentBoardPosition
        {
            get { return _currentBoardPosition; }
            set { Set(() => CurrentBoardPosition, ref _currentBoardPosition, value); }
        }

        private int _currentCircuitPosition;

        public int CurrentCircuitPosition
        {
            get { return _currentCircuitPosition; }
            set { Set(() => CurrentCircuitPosition, ref _currentCircuitPosition, value); }
        }

        private int _currentEndPointPosition;

        public int CurrentEndPointPosition
        {
            get { return _currentEndPointPosition; }
            set { Set(() => CurrentEndPointPosition, ref _currentEndPointPosition, value); }
        }

        private bool _circuitHasAnyCircuit;

        public bool CircuitHasAnyCircuit
        {
            get { return _circuitHasAnyCircuit; }
            set { Set(() => CircuitHasAnyCircuit, ref _circuitHasAnyCircuit, value); }
        }

        private BoardTest _selectedBoard;

        public BoardTest SelectedBoard
        {
            get { return _selectedBoard; }
            set
            {
                Set(() => SelectedBoard, ref _selectedBoard, value);
            }
        }

        private CircuitTest _selectedCircuit;

        public CircuitTest SelectedCircuit
        {
            get { return _selectedCircuit; }
            set
            {
                Set(() => SelectedCircuit, ref _selectedCircuit, value);
            }
        }

        private bool _inCode;

        private RelayCommand _openEditPointDetailsCommand;

        public RelayCommand OpenEditPointDetailsCommand
        {
            get
            {
                return _openEditPointDetailsCommand ?? (_openEditPointDetailsCommand = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<EditPointDetailsViewModel>(new NavigationDataEndPoint
                        {
                            BoardId = SelectedBoard.BoardId,
                            CircuitId = SelectedCircuit.CircuitId,
                            CircuitPhase = SelectedCircuit.CircuitPhase,
                            CircuitReference = SelectedCircuit.CircuitReference,
                            CircuitTestId = SelectedCircuit.CircuitTestId
                        });
                    }
                }));
            }
        }

        private RelayCommand _indexBoardsChangedCommand;

        public RelayCommand IndexBoardsChangedCommand
        {
            get
            {
                return _indexBoardsChangedCommand ?? (_indexBoardsChangedCommand = new RelayCommand(async () =>
                {
                    await GetCircuitsForCurrentBoard();
                }, () => !_inCode));
            }
        }

        private RelayCommand _indexCircuitsChangedCommand;

        public RelayCommand IndexCircuitsChangedCommand
        {
            get
            {
                return _indexCircuitsChangedCommand ?? (_indexCircuitsChangedCommand = new RelayCommand(GetPointsForCurrentCircuit, () => !_inCode));
            }
        }

        private RelayCommand _boardSelectedCommand;

        public RelayCommand BoardSelectedCommand
        {
            get
            {
                return _boardSelectedCommand ?? (_boardSelectedCommand = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        if (SelectedBoard == null)
                            return;

                        await NavigationService.NavigateToAsync<MainBoardDataViewModel>(SelectedBoard.BoardId);
                    }
                }));
            }
        }

        private RelayCommand _circuitSelectedCommand;

        public RelayCommand CircuitSelectedCommand
        {
            get
            {
                return _circuitSelectedCommand ?? (_circuitSelectedCommand = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        if (SelectedCircuit == null)
                            return;

                        await NavigationService.NavigateToAsync<MainCircuitDataViewModel>(SelectedCircuit.CircuitTestId);
                    }
                }));
            }
        }

        private RelayCommand _previousBoardCommand;

        public RelayCommand PreviousBoardCommand
        {
            get
            {
                return _previousBoardCommand ?? (_previousBoardCommand = new RelayCommand(async () =>
                {
                    if (CurrentBoardPosition > 0)
                    {
                        CurrentBoardPosition--;
                        await GetCircuitsForCurrentBoard();
                    }
                }));
            }
        }

        private RelayCommand _nextBoardCommand;

        public RelayCommand NextBoardCommand
        {
            get
            {
                return _nextBoardCommand ?? (_nextBoardCommand = new RelayCommand(async () =>
                {
                    if (CurrentBoardPosition < Boards.Count - 1)
                    {
                        CurrentBoardPosition++;
                        await GetCircuitsForCurrentBoard();
                    }
                }));
            }
        }

        private RelayCommand _previousCircuitCommand;

        public RelayCommand PreviousCircuitCommand
        {
            get
            {
                return _previousCircuitCommand ?? (_previousCircuitCommand = new RelayCommand(() =>
                {
                    if (CurrentCircuitPosition > 0)
                    {
                        CurrentCircuitPosition--;
                        GetPointsForCurrentCircuit();
                    }
                }));
            }
        }

        private RelayCommand _nextCircuitCommand;

        public RelayCommand NextCircuitCommand
        {
            get
            {
                return _nextCircuitCommand ?? (_nextCircuitCommand = new RelayCommand(() =>
                {
                    if (CurrentCircuitPosition < Circuits.Count - 1)
                    {
                        CurrentCircuitPosition++;
                        GetPointsForCurrentCircuit();
                    }
                }));
            }
        }

        private bool _showArrows;

        public bool ShowArrows
        {
            get { return _showArrows; }
            set { Set(() => ShowArrows, ref _showArrows, value); }
        }

        private async Task GetCircuitsForCurrentBoard()
        {
            try
            {
                _inCode = true;

                if (Boards != null && Boards.Any() && CurrentBoardPosition == -1)
                    CurrentBoardPosition = 0;

                if (Boards != null) SelectedBoard = Boards[CurrentBoardPosition];
                var result = await _boardsService.GetTestedCircuits(CertificateId, SelectedBoard.BoardId);
                if (!result.Success)
                {
                    Dialogs.Toast(result.Message);
                    return;
                }

                var circuitTest = result.ResultObject as List<CircuitTest>;
                if (circuitTest == null)
                    return; 

                CurrentCircuitPosition = 0;
                Circuits.Clear();

                var circuits = new List<CircuitTest>();

                if (SelectedBoard.BoardPhase == "3" || SelectedBoard.BoardPhase == "TP&N")
                {
                    var numberToSkip = 0;
                    var numbers = circuitTest.Select(x => x.CircuitReference).Distinct();
                    var threePhaseCircutes = new List<int>();

                    foreach (var number in numbers)
                    {
                        if(circuitTest.Any(x => x.CircuitReference == number && x.CircuitIs3Phase == "Y"))
                            threePhaseCircutes.Add(number);
                    }

                    if (!threePhaseCircutes.Any())
                    {
                        circuits.AddRange(circuitTest);
                    }
                    else
                    {
                        foreach (var circuit in circuitTest)
                        {
                            if (circuit.CircuitReference == numberToSkip)
                                continue;

                            if (threePhaseCircutes.Contains(circuit.CircuitReference))
                            {
                                if (circuit.CircuitIs3Phase == "Y")
                                {
                                    circuit.NotBelongsToThreePhase = false;
                                    circuit.ThreePhase = circuit.CircuitReference + SelectedBoard.BoardCircuitPhaseNaming;
                                    circuits.Add(circuit);
                                    numberToSkip = circuit.CircuitReference;
                                }
                                continue;
                            }
                            circuit.NotBelongsToThreePhase = true;
                            circuits.Add(circuit);
                        }
                    }
                }
                else
                {
                    circuits.AddRange(circuitTest);
                }

                Circuits = new ObservableCollectionFast<CircuitTest>(circuits.OrderBy(x => x.ThreePhaseRepresentation));
                
                //Circuits.AddRange(new List<CircuitTest>(circuitTest.OrderBy(x => x.CircuitIdentity)));

                GetPointsForCurrentCircuit();

                _inCode = false;
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await GetCircuitsForCurrentBoard();
            }
            catch (Exception e)
            {
                //await ShowErrorAlert(e.Message);
            }
        }

        private void GetPointsForCurrentCircuit()
        {
            _inCode = true;

            try
            {
                if (Circuits != null && Circuits.Any() && CurrentCircuitPosition == -1)
                    CurrentCircuitPosition = 0;
                if (Circuits != null) SelectedCircuit = Circuits[CurrentCircuitPosition];
                RaisePropertyChanged(() => CurrentCircuitPosition);
            }
            catch (Exception e)
            {

            }

            _inCode = false;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            //await GetBoards();
        }

        public async Task GetBoards()
        {
            Boards.Clear();

            var result = await _boardsService.GetTestedBoards(CertificateId);
            if (!result.Success)
            {
                Dialogs.Toast(result.Message);
                return;
            }

            var boards = result.ResultObject as List<BoardTest>;
            if (boards == null || !boards.Any())
                return;

            Boards.AddRange(new List<BoardTest>(boards.OrderBy(x => x.BoardIdentity)));
            await GetCircuitsForCurrentBoard();
        }

        public override async Task OnAppearing()
        {
            ShowArrows = false;

            await GetBoards();
            RaisePropertyChanged(() => CurrentBoardPosition);
            RaisePropertyChanged(() => CurrentCircuitPosition);
            
            ShowArrows = true;
        }

        public EditPointViewModel(IBoardsService boardsService)
        {
            Title = "Edit Point";

            _boardsService = boardsService;
            Boards = new ObservableCollectionFast<BoardTest>();
            Circuits = new ObservableCollectionFast<CircuitTest>();

            ShowArrows = true;
        }
    }
}