using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain;
using EDIS.Domain.Boards;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Boards
{
    public class AllBoardsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;

        public List<BoardTest> TestedBoards { get; set; }

        public ObservableCollectionFast<BoardSelect> Boards { get; set; }

        private RelayCommand<BoardSelect> _boardSelectedCommand;

        public RelayCommand<BoardSelect> BoardSelectedCommand
        {
            get
            {
                return _boardSelectedCommand ?? (_boardSelectedCommand = new RelayCommand<BoardSelect>(board =>
                {
                    if (board == null)
                        return;

                    board.IsSelected = !board.IsSelected;
                }));
            }
        }

        private RelayCommand _importBoardsCommand;

        public RelayCommand ImportBoardsCommand
        {
            get
            {
                return _importBoardsCommand ?? (_importBoardsCommand = new RelayCommand(async () =>
                {
                    using (var d = Dialogs.Loading("Progress"))
                    {
                        foreach (var board in Boards)
                        {
                            if (TestedBoards.Any(x => x.BoardId == board.Board.BoardId))
                            {
                                if (board.IsSelected) continue;
                            }

                            if (board.IsSelected)
                            {
                                await _boardsService.GetBoardDetails(board.Board.BoardId, BuildingId, true);
                            }
                        }

                        await NavigationService.NavigateBackAsync();
                    }
                }));
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var filters = navigationData as BoardsFilters;

            await GetBoards(filters, true);

            await NavigationService.RemoveLastFromBackStackAsync();
        }

        private async Task GetBoards(BoardsFilters filters, bool forceCloud = false)
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

                        var allBoardsResult = await _boardsService.GetAllBoards(filters, BuildingId, forceCloud);
                        if (!allBoardsResult.Success)
                        {
                            Dialogs.Toast(allBoardsResult.Message);
                            return;
                        }

                        var boardsAndNumber = allBoardsResult.ResultObject as Tuple<IEnumerable<BoardRow>, int, int>;
                        if(boardsAndNumber == null)
                            return;

                        Dialogs.Toast("Results; " + boardsAndNumber.Item3 + " records returned out of " + boardsAndNumber.Item2 + " found.");

                        var boards = boardsAndNumber.Item1;

                        if (boards != null && boards.Any())
                        {
                            foreach (var board in boards)
                            {
                                var selectableBoard = new BoardSelect {Board = board, IsSelected = false};
                                if (relatedBoards.Any(x => x.BoardId == board.BoardId))
                                    selectableBoard.IsSelected = true;
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
                    await GetBoards(filters, forceCloud);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public AllBoardsViewModel(IBoardsService boardsService)
        {
            Title = "Boards on Cloud";

            _boardsService = boardsService;
            Boards = new ObservableCollectionFast<BoardSelect>();
            TestedBoards = new List<BoardTest>();
        }
    }
}