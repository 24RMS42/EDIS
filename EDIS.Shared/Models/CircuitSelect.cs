using EDIS.Domain.Circuits;
using EDIS.Shared.ViewModels.Base;

namespace EDIS.Shared.Models
{
    public class CircuitSelect : BaseViewModel
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        private Circuit _circuit;

        public Circuit Circuit
        {
            get { return _circuit; }
            set { Set(() => Circuit, ref _circuit, value); }
        }
    }
}