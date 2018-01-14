using System.Threading.Tasks;
using EDIS.Shared.Pages.Login;
using EDIS.Shared.Services.Interfaces;
using EDIS.Shared.ViewModels.Base;
using Xamarin.Forms;

namespace EDIS.Shared
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            var initializeAsync = navigationService?.InitializeAsync();
            if (initializeAsync != null) await initializeAsync;
        }
    }
}
