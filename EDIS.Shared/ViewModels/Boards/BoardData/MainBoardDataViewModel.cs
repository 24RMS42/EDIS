using System;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Boards;
using EDIS.Service.Interfaces;
using EDIS.Shared.Helpers;
using EDIS.Shared.Pages.Boards.BoardData;
using EDIS.Shared.Pages.Boards.BoardDetails;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards.BoardDetails;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Boards.BoardData
{
    public class MainBoardDataViewModel : BaseViewModel
    {
        private string _boardId;

        private readonly IBoardsService _boardsService;

        public BasicInfoViewModel BoardViewModel { get; set; }
        public BoardTestDetailsViewModel BoardTestViewModel { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<BasicInfoPage, string>(this, MessengerCenterMessages.ChangeTitleOnBoardInfoTab, (data, s) =>
            {
                Title = s;
            });
            MessagingCenter.Subscribe<BoardTestDetailsPage, string>(this, MessengerCenterMessages.ChangeTitleOnBoardInfoTab, (data, s) =>
            {
                Title = s;
            });

            var boardId = navigationData as string;
            if (boardId != null)
                _boardId = boardId;

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    //var boardResult = await _boardsService.GetBoardDetails(_boardId, BuildingId);
                    //if (!boardResult.Success)
                    //{
                    //    Dialogs.Toast(boardResult.Message);
                    //    return;
                    //}

                    //var board = boardResult.ResultObject as Board;
                    //if (board != null)
                    //{
                    //    BoardViewModel.Board = board;
                    //}

                    var boardTestResult = await _boardsService.GetBoardTestDetails(_boardId, CertificateId);
                    if (!boardTestResult.Success)
                    {
                        Dialogs.Toast(boardTestResult.Message);
                        return;
                    }

                    var boardTest = boardTestResult.ResultObject as BoardTest;
                    if (boardTest != null)
                    {
                        BoardTestViewModel.Board = boardTest;
                        BoardViewModel.Board = boardTest;

                        await BoardViewModel.InitializeAsync(null);
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

        public MainBoardDataViewModel(IBoardsService boardsService, ILookupsService lookupsService)
        {
            Title = "Board Details - Board Test";
            _boardsService = boardsService;
            BoardTestViewModel = new BoardTestDetailsViewModel(_boardsService);
            BoardViewModel = new BasicInfoViewModel(_boardsService, lookupsService);
        }
    }
}