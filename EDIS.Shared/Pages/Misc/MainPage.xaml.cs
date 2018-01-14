using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Certificates;
using Xamarin.Forms;

namespace EDIS.Shared.Pages.Misc
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<AssociatedBoardsViewModel>(this, MessengerCenterMessages.HideBackOnCertificateDetails,
                model =>
                {
                    IsGestureEnabled = false;
                });

            MessagingCenter.Subscribe<AssociatedBoardsViewModel>(this, MessengerCenterMessages.ShowBackOnCertificateDetails,
                model =>
                {
                    IsGestureEnabled = true;
                });
        }
    }
}