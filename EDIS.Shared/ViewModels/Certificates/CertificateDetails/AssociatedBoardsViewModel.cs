using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EDIS.Core.Exceptions;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards;
using EDIS.Shared.ViewModels.Boards.BoardDetails;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class AssociatedBoardsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;
        private bool _synced = true;

        public ObservableCollectionFast<BoardTest> AssociatedBoards { get; set; }

        private RelayCommand _addRemoveBoardsCommand;

        public RelayCommand AddRemoveBoardsCommand
        {
            get
            {
                return _addRemoveBoardsCommand ?? (_addRemoveBoardsCommand = new RelayCommand(async () =>
                {
                    _synced = false;

                    MessagingCenter.Subscribe<AddRemoveBoardsViewModel>(this, MessengerCenterMessages.RefreshBoardList,
                        async model =>
                        {
                            await Task.Delay(1000);
                            await RefreshBoards();
                            await CheckDoesEveryBoardHasAssociatedCircuit();
                            MessagingCenter.Unsubscribe<AddRemoveBoardsViewModel>(this, MessengerCenterMessages.RefreshBoardList);
                        });

                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<AddRemoveBoardsViewModel>();
                    }
                }));
            }
        }

        private bool _onCircuitsPage = false;

        private RelayCommand<BoardTest> _boardSelectedCommand;

        public RelayCommand<BoardTest> BoardSelectedCommand
        {
            get
            {
                return _boardSelectedCommand ?? (_boardSelectedCommand = new RelayCommand<BoardTest>(async (board) =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        _onCircuitsPage = true;
                        await NavigationService.NavigateToAsync<BoardViewModel>(board.BoardId);
                    }

                    MessagingCenter.Subscribe<BoardViewModel>(this, MessengerCenterMessages.RefreshBoardList,
                            async model =>
                            {
                                await CheckDoesEveryBoardHasAssociatedCircuit();
                                MessagingCenter.Unsubscribe<BoardViewModel>(this,
                                    MessengerCenterMessages.RefreshBoardList);
                                _onCircuitsPage = false;
                            });
                }));
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await RefreshBoards();
        }

        private bool _doesEveryBoardHasAssociatedCircuit = true;

        public bool DoesEveryBoardHasAssociatedCircuit
        {
            get { return _doesEveryBoardHasAssociatedCircuit; }
            set { Set(() => DoesEveryBoardHasAssociatedCircuit, ref _doesEveryBoardHasAssociatedCircuit, value); }
        }

        public string ListOfBoardsWithoutAssociatedCircuit;

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { Set(() => ErrorMessage, ref _errorMessage, value); }
        }

        public void ShowError()
        {
            var message = "The following board(s) have no circuits selected: " + ListOfBoardsWithoutAssociatedCircuit +
                          " Each of these board(s) requires at least 1 circuit to be selected. " +
                          "You could also consider removing the board from the certificate, by de-associating it.";
            Dialogs.Confirm(new ConfirmConfig {Message = message, CancelText = "", OnAction = b => {}, Title = "Warning", OkText = "OK"});
        }

        public async Task CheckDoesEveryBoardHasAssociatedCircuit()
        {
            try
            {
                var listOfBoards = "";
                DoesEveryBoardHasAssociatedCircuit = true;

                var testedBoardsResult = await _boardsService.GetTestedBoards(CertificateId);
                if (!testedBoardsResult.Success)
                {
                    Dialogs.Toast(testedBoardsResult.Message);
                    return;
                }

                var boards = testedBoardsResult.ResultObject as List<BoardTest>;
                if (boards != null && boards.Any())
                {
                    foreach (var board in boards)
                    {
                        var circuitResult = await _boardsService.GetTestedCircuits(CertificateId, board.BoardId);
                        if (!circuitResult.Success)
                        {
                            Dialogs.Toast(circuitResult.Message);
                            return;
                        }

                        var circuitTest = circuitResult.ResultObject as List<CircuitTest>;
                        if (circuitTest != null)
                        {
                            if (circuitTest.Any())
                                continue;

                            DoesEveryBoardHasAssociatedCircuit = false;
                            listOfBoards += board.BoardIdentity + ", ";
                        }
                    }
                    if (!string.IsNullOrEmpty(listOfBoards))
                    {
                        listOfBoards += ".";
                        listOfBoards = listOfBoards.Replace(", .", ".");

                        ListOfBoardsWithoutAssociatedCircuit = listOfBoards;
                        ErrorMessage = "Following boards: " + ListOfBoardsWithoutAssociatedCircuit +
                                       " Has no circuit selected, please select a board and then associate a circuit!";

                        ShowError();
                    }

                    MessagingCenter.Send(this,
                        DoesEveryBoardHasAssociatedCircuit
                            ? MessengerCenterMessages.ShowBackOnCertificateDetails
                            : MessengerCenterMessages.HideBackOnCertificateDetails);
                }
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public override async Task OnAppearing()
        {
            if(!_onCircuitsPage)
                await CheckDoesEveryBoardHasAssociatedCircuit();
        }

        private async Task RefreshBoards()
        {
            try
            {
                using (var d = Dialogs.Loading("Progress"))
                {
                    var testedBoardsResult = await _boardsService.GetTestedBoards(CertificateId);
                    if (!testedBoardsResult.Success)
                    {
                        Dialogs.Toast(testedBoardsResult.Message);
                        return;
                    }

                    var boards = testedBoardsResult.ResultObject as List<BoardTest>;
                    if (boards != null && boards.Any())
                    {
                        AssociatedBoards.Clear();
                        AssociatedBoards.AddRange(boards);
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await RefreshBoards();
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public AssociatedBoardsViewModel(IBoardsService boardsService)
        {
            Title = "Associated Boards";

            _boardsService = boardsService;
            AssociatedBoards = new ObservableCollectionFast<BoardTest>();
        }
    }
}