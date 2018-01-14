using System.Collections.Generic;
using EDIS.Domain.Boards;
using EDIS.Service.Interfaces;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Boards.BoardData
{
    public class BoardTestDetailsViewModel : BaseViewModel
    {
        private readonly IBoardsService _boardsService;

        private int _selectedPolarity;

        public int SelectedPolarity
        {
            get => Board.CorrectSupplyPolarityConfirmed ?? 0;
            set => Board.CorrectSupplyPolarityConfirmed = value;
        }

        private int _selectedPhase;

        public int SelectedPhase
        {
            get => Board.PhaseSequenceConfirmed ?? 0;
            set => Board.PhaseSequenceConfirmed = value;
        }

        private List<PolarityValue> _polarityValues;

        public List<PolarityValue> PolarityValues => new List<PolarityValue>
        {
            new PolarityValue
            {
                Value = 0,
                Text = "Blank"
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
                Text = "Tick"
            },
            new PolarityValue
            {
                Value = 4,
                Text = "Cross"
            }
        };

        private BoardTest _board;

        public BoardTest Board
        {
            get => _board;
            set
            {
                Set(() => Board, ref _board, value);
                RaisePropertyChanged(() => SelectedPolarity);
                RaisePropertyChanged(() => SelectedPhase);
            }
        }

        private RelayCommand _save;

        public RelayCommand Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand(async () =>
                {
                    await _boardsService.SaveBoardTest(Board);

                    Dialogs.Toast("Successful");
                }));
            }
        }

        public BoardTestDetailsViewModel(IBoardsService boardsService)
        {
            Title = "Board Test Details";

            _boardsService = boardsService;
            Board = new BoardTest();
        }
    }
}