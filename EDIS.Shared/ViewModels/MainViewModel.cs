using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using EDIS.Shared.Services;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Certificates;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MenuViewModel _menuViewModel;

        public MenuViewModel MenuViewModel
        {
            get { return _menuViewModel; }
            set { Set(() => MenuViewModel, ref _menuViewModel, value); }
        }

        public MainViewModel(MenuViewModel menuViewModel)
        {
            Title = "Home";
            _menuViewModel = menuViewModel;
        }

        public override Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<CertificatesViewModel>(this, MessengerCenterMessages.RefreshApiOnMenu, async (model) =>
            {
                await MenuViewModel.Refresh();
            });

            return Task.WhenAll
            (
                _menuViewModel.InitializeAsync(navigationData),
                NavigationService.NavigateToAsync<CertificatesViewModel>()
            );
        }
    }
}