using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using EDIS.Shared.Pages.Certificates.CertificateDetails;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Points
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPointPage : ContentPage, ICertificateDetails
    {
        public EditPointPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, "Certificate Details - Test");
            base.OnAppearing();
        }

        private void TapGestureRecognizer_OnTappedBoards(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PickerBoards.Focus();
            });
        }

        private void TapGestureRecognizer_OnTappedCircuits(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PickerCircuits.Focus();
            });
            
        }
    }
}