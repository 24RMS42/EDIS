using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Pages.Base;
using EDIS.Shared.ViewModels.Profile;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : TabbedPage
    {
		public ProfilePage ()
        {
            InitializeComponent();

            /*MessagingCenter.Subscribe<BasicInfoViewModel>(this, MessengerCenterMessages.HideBackOnCertificateDetails,
                model =>
                {
                    NavigationPage.SetHasBackButton(this, false);
                });

            MessagingCenter.Subscribe<ElectricianInfoViewModel>(this, MessengerCenterMessages.ShowBackOnCertificateDetails,
                model =>
                {
                    NavigationPage.SetHasBackButton(this, true);
                });*/
        }

        protected override bool OnBackButtonPressed()
        {
            var viewModel = BindingContext as ProfileViewModel;
            return viewModel != null;
        }
    }

}