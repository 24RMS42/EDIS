using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain;
using EDIS.Domain.Boards;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards.BoardDetails;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Boards
{
    public class AddRemoveBoardsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;

        private bool _onNextPage;

        public List<BoardTest> TestedBoards { get; set; }

        public ObservableCollectionFast<BoardDetailsSelect> Boards { get; set; }

        private RelayCommand<BoardDetailsSelect> _boardSelectedCommand;

        public RelayCommand<BoardDetailsSelect> BoardSelectedCommand
        {
            get
            {
                return _boardSelectedCommand ?? (_boardSelectedCommand = new RelayCommand<BoardDetailsSelect>(board =>
                {
                    if (board == null)
                        return;

                    board.IsSelected = !board.IsSelected;
                }));
            }
        }

        private RelayCommand _refresh;

        public RelayCommand Refresh
        {
            get
            {
                return _refresh ?? (_refresh = new RelayCommand(async () =>
                {
                    _onNextPage = true;
                    await NavigationService.NavigateToAsync<FilterBoardsViewModel>();
                }));
            }
        }

        public override async Task OnAppearing()
        {
            _onNextPage = false;
            await GetBoards();
        }

        public override async Task OnDisappearing()
        {
            if(_onNextPage)
                return;

            await Import();
        }

        private async Task GetBoards()
        {
            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    Boards.Clear();

                    var testedBoardsResult = await _boardsService.GetTestedBoards(CertificateId);
                    if (!testedBoardsResult.Success)
                    {
                        Dialogs.Toast(testedBoardsResult.Message);
                        return;
                    }

                    var relatedBoards = testedBoardsResult.ResultObject as List<BoardTest>;

                    if (relatedBoards != null)
                    {
                        TestedBoards = relatedBoards;

                        var downloadedBoardsResult = await _boardsService.GetAllDownloadedBoards(BuildingId);
                        if (!downloadedBoardsResult.Success)
                        {
                            Dialogs.Toast(downloadedBoardsResult.Message);
                            return;
                        }

                        var boards = downloadedBoardsResult.ResultObject as List<Board>;

                        if (boards != null && boards.Any())
                        {
                            foreach (var board in boards)
                            {
                                var selectableBoard = new BoardDetailsSelect {Board = board, IsSelected = false};

                                if (relatedBoards.Any(x => x.BoardId == board.BoardId))
                                    selectableBoard.IsSelected = true;
                                Boards.Add(selectableBoard);
                            }

                            foreach (var board in relatedBoards)
                            {
                                if (Boards.Any(x => x.Board.BoardId == board.BoardId))
                                    continue;

                                var testedBoard = AutoMapper.Mapper.Map<Board>(board);
                                var selectableBoard = new BoardDetailsSelect {Board = testedBoard, IsSelected = true};
                                Boards.Add(selectableBoard);
                            }
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
                    await GetBoards();
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public async Task Import()
        {
            var errorCheck = false;
            try
            {
                using (var d = Dialogs.Loading("Progress"))
                {
                    foreach (var board in Boards)
                    {
                        if (TestedBoards.Any(x => x.BoardId == board.Board.BoardId))
                        {
                            if (board.IsSelected) continue;

                            var result = await _boardsService.RemoveTestedBoard(board.Board.BoardId, CertificateId);
                            if (!result.Success)
                                errorCheck = true;
                        }

                        if (board.IsSelected)
                        {
                            var result = await _boardsService.AssociateBoardWithCertificate(board.Board.BoardId, BuildingId,
                                CertificateId);
                            if (!result.Success)
                                errorCheck = true;
                        }
                    }

                    MessagingCenter.Send(this, MessengerCenterMessages.RefreshBoardList);

                    if (errorCheck)
                    {
                        Dialogs.Toast("Error occured");
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await Import();
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public AddRemoveBoardsViewModel(IBoardsService boardsService)
        {
            Title = "Add/Remove Board";

            _boardsService = boardsService;
            Boards = new ObservableCollectionFast<BoardDetailsSelect>();
            TestedBoards = new List<BoardTest>();
        }
    }
}