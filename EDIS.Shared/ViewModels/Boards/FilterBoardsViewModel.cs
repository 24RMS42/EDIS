using EDIS.Domain;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Boards
{
    public class FilterBoardsViewModel : BaseViewModel
    {
        private BoardsFilters _boardsFilters;

        public BoardsFilters Filters
        {
            get { return _boardsFilters; }
            set { Set(() => Filters, ref _boardsFilters, value); }
        }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(async () =>
                {
                    await NavigationService.NavigateBackAsync();
                }));
            }
        }

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(async () =>
                {
                    await NavigationService.NavigateToAsync<AllBoardsViewModel>(Filters);
                }));
            }
        }

        public FilterBoardsViewModel()
        {
            Title = "Filter Boards";

            Filters = new BoardsFilters();
        }
    }
}