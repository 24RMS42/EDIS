using EDIS.Domain.Boards;
using EDIS.Shared.ViewModels.Base;

namespace EDIS.Shared.Models
{
    public class BoardSelect : BaseViewModel
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        private BoardRow _board;

        public BoardRow Board
        {
            get { return _board; }
            set { Set(() => Board, ref _board, value); }
        }
    }
}