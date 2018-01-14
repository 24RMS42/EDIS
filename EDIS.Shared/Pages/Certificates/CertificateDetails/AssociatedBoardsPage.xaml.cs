using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Certificates.CertificateDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssociatedBoardsPage : ContentPage, ICertificateDetails
    {
        public AssociatedBoardsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, "Certificate Details - Associated Board");
            base.OnAppearing();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            arrow.RotateTo(arrow.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);

            if(Device.RuntimePlatform == Device.Android)
                warning.TranslateTo(0, warning.TranslationY == 0 ? warning.TranslationY + errorMessage.Height : 0, 250U, Easing.Linear);
            else
                warning.TranslateTo(0, warning.TranslationY == 0 ? warning.TranslationY + errorMessage.Height : 0, 250U, Easing.Linear);
        }
    }
}