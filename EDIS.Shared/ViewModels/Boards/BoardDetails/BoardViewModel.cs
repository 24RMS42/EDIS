using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using EDIS.Service.Interfaces;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.Pages.Boards.BoardData;
using EDIS.Shared.Pages.Boards.BoardDetails;
using EDIS.Shared.ViewModels.Base;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Boards.BoardDetails
{
    public class BoardViewModel : BaseViewModel
    {
        private string _boardId;
        private readonly IBoardsService _boardsService;

        public BasicInfoViewModel BasicInfoViewModel { get; set; }
        public CircuitsViewModel CircuitsViewModel { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<BasicInfoPage, string>(this, MessengerCenterMessages.ChangeTitleOnBoardAssociatedTab, (details, s) =>
            {
                Title = s;
            });
            MessagingCenter.Subscribe<CircuitsPage, string>(this, MessengerCenterMessages.ChangeTitleOnBoardAssociatedTab, (details, s) =>
            {
                Title = s;
            });

            var boardId = navigationData as string;
            if (boardId != null)
                _boardId = boardId;

            await Refresh();
        }

        private async Task Refresh()
        {
            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var boardResult = await _boardsService.GetBoardDetails(_boardId, BuildingId);
                    var boardTestResult = await _boardsService.GetBoardTestDetails(_boardId, CertificateId);
                    if (!boardResult.Success || !boardResult.Success)
                    {
                        Dialogs.Toast(boardResult.Message);
                        return;
                    }

                    var board = boardResult.ResultObject as Board;
                    var boardTest = boardTestResult.ResultObject as BoardTest;
                    if (board != null)
                    {
                        BasicInfoViewModel.Board = boardTest;

                        var circuitResult = await _boardsService.GetTestedCircuits(CertificateId, board.BoardId);
                        if (!circuitResult.Success)
                        {
                            Dialogs.Toast(circuitResult.Message);
                            return;
                        }

                        var circuits = new List<CircuitTest>();

                        var circuitTest = circuitResult.ResultObject as List<CircuitTest>;
                        if (circuitTest != null)
                        {
                            CircuitsViewModel.TestedCircuits.Clear();
                            CircuitsViewModel.Circuits.Clear();

                            CircuitsViewModel.TestedCircuits = circuitTest;

                            foreach (var circuit in board.Circuits)
                            {
                                if (board.BoardPhase == "3" || board.BoardPhase == "TP&N")
                                {
                                    if (circuitTest.FirstOrDefault(x => x.CircuitReference == circuit.CircuitReference)?.CircuitIs3Phase == "Y")
                                    {
                                        if (CircuitsViewModel.Circuits.Any(x => x.Circuit.CircuitReference == circuit.CircuitReference))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            circuit.NotBelongsToThreePhase = false;
                                            circuit.ThreePhase = circuit.CircuitReference + board.BoardCircuitPhaseNaming;
                                            CircuitsViewModel.Circuits.Add(new CircuitSelect
                                            {
                                                Circuit = circuit,
                                                IsSelected = circuitTest.Any(x => x.CircuitId == circuit.CircuitId && x.CircuitReference == circuit.CircuitReference)
                                            });
                                            continue;
                                        }
                                    }
                                }

                                CircuitsViewModel.Circuits.Add(new CircuitSelect
                                {
                                    Circuit = circuit,
                                    IsSelected = circuitTest.Any(x => x.CircuitId == circuit.CircuitId)
                                });
                            }

                            CircuitsViewModel.Refresh();

                            await BasicInfoViewModel.InitializeAsync(null);
                        }
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await InitializeAsync(_boardId);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public override async Task OnAppearing()
        {
            await Refresh();
        }

        public override async Task OnDisappearing()
        {
            await CircuitsViewModel.OnDisappearing();
            await BasicInfoViewModel.OnDisappearing();

            MessagingCenter.Send(this, MessengerCenterMessages.RefreshBoardList);
        }

        public BoardViewModel(IBoardsService boardsService, ILookupsService lookupsService)
        {
            Title = "Board Details - Circuits";

            _boardsService = boardsService;

            BasicInfoViewModel = new BasicInfoViewModel(boardsService, lookupsService);
            CircuitsViewModel = new CircuitsViewModel(boardsService);
        }
    }
}