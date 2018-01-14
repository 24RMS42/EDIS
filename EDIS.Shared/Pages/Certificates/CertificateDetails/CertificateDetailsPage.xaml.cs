using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using EDIS.Shared.Pages.Base;
using EDIS.Shared.ViewModels.Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Certificates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CertificateDetailsPage : TabbedPage
    {
        public CertificateDetailsPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<AssociatedBoardsViewModel>(this, MessengerCenterMessages.HideBackOnCertificateDetails,
                model =>
                {
                    NavigationPage.SetHasBackButton(this, false);
                });

            MessagingCenter.Subscribe<AssociatedBoardsViewModel>(this, MessengerCenterMessages.ShowBackOnCertificateDetails,
                model =>
                {
                    NavigationPage.SetHasBackButton(this, true);
                });
        }

        protected override bool OnBackButtonPressed()
        {
            var viewModel = BindingContext as CertificateViewModel;
            return viewModel != null && !viewModel.AssociatedBoardsViewModel.DoesEveryBoardHasAssociatedCircuit;
        }
    }
}