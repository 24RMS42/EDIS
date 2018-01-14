using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Circuits;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Points;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ObjectBuilder2;

namespace EDIS.Shared.ViewModels.Boards.BoardDetails
{
    public class CircuitsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;

        public List<CircuitTest> TestedCircuits { get; set; }
        public ObservableCollectionFast<CircuitSelect> Circuits { get; set; }

        public void Refresh()
        {
            RaisePropertyChanged(() => Circuits);
        }

        private bool _showCancel;

        public bool ShowCancel
        {
            get { return _showCancel; }
            set { Set(() => ShowCancel, ref _showCancel, value); }
        }

        private bool _selectAll;

        public bool SelectAll
        {
            get { return _selectAll; }
            set
            {
                Set(() => SelectAll, ref _selectAll, value); 
                
                Circuits?.ForEach(x => x.IsSelected = value);
            }
        }

        private RelayCommand<CircuitSelect> _circuitSelectedToMoveCommand;

        public RelayCommand<CircuitSelect> CircuitSelectedToMoveCommand
        {
            get
            {
                return _circuitSelectedToMoveCommand ?? (_circuitSelectedToMoveCommand = new RelayCommand<CircuitSelect>(async (circuit) =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<EditPointViewModel>();
                    }
                }));
            }
        }

        private RelayCommand<CircuitSelect> _circuitSelectedCommand;

        public RelayCommand<CircuitSelect> CircuitSelectedCommand
        {
            get
            {
                return _circuitSelectedCommand ?? (_circuitSelectedCommand = new RelayCommand<CircuitSelect>((circuit) =>
                {
                    if (circuit == null)
                        return;

                    circuit.IsSelected = !circuit.IsSelected;
                }));
            }
        }

        private RelayCommand _cancelCheckedCircuitsCommand;

        public RelayCommand CancelCheckedCircuitsCommand
        {
            get { return _cancelCheckedCircuitsCommand ?? (_cancelCheckedCircuitsCommand = new RelayCommand(UncheckSelectedCircuits)); }
        }

        private void UncheckSelectedCircuits()
        {
            try
            {
                using (var d = Dialogs.Loading("Progress"))
                {
                    foreach (var circuit in Circuits)
                    {
                        circuit.IsSelected = TestedCircuits.Any(x => x.CircuitId == circuit.Circuit.CircuitId);
                    }
                }
            }
            catch (Exception e)
            {
                Dialogs.Toast("Error occured");
            }
        }

        private async Task CheckSelectedCircuits()
        {
            try
            {
                var errorCheck = false;

                using (var d = Dialogs.Loading("Progress"))
                {
                    foreach (var circuit in Circuits)
                    {
                        if (TestedCircuits.Any(x => x.CircuitId == circuit.Circuit.CircuitId))
                        {
                            if (circuit.IsSelected) continue;

                            var result = await _boardsService.RemoveTestedCircuit(circuit.Circuit.CircuitId,
                                circuit.Circuit.BoardId,
                                CertificateId);

                            if (!result.Success)
                                errorCheck = true;
                        }

                        if (circuit.IsSelected)
                        {
                            var result = await _boardsService.AddNewCircuitForTest(circuit.Circuit.CircuitId,
                                circuit.Circuit.BoardId, CertificateId);

                            if (!result.Success)
                                errorCheck = true;
                        }
                    }
                }

                if (errorCheck)
                {
                    Dialogs.Toast("Error occured");
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await CheckSelectedCircuits();
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public override async Task OnDisappearing()
        {
            await CheckSelectedCircuits();
        }

        public CircuitsViewModel(IBoardsService boardsService)
        {
            Title = "Circuits";

            _boardsService = boardsService;
            TestedCircuits = new List<CircuitTest>();
            Circuits = new ObservableCollectionFast<CircuitSelect>();
        }
    }
}