using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Boards.Circuits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircuitDetailsPage : ContentPage, ICircuit
    {
        private double _circuitDetailsHeight = 960;
        private double _overCurrentProtectionHeight = 530;
        private double _circuitRcdHeight = 270;

        public CircuitDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnCircuitTab, "Circuit Details - Circuit");
            base.OnAppearing();
        }

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            CircuitDetails.ScaleTo(Math.Abs(CircuitDetails.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            CircuitDetails.HeightRequest = Math.Abs(CircuitDetails.HeightRequest) < 1 ? _circuitDetailsHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            OverCurrentProtection.ScaleTo(Math.Abs(OverCurrentProtection.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            OverCurrentProtection.HeightRequest = Math.Abs(OverCurrentProtection.HeightRequest) < 1 ? _overCurrentProtectionHeight : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            CircuitRcd.ScaleTo(Math.Abs(CircuitRcd.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            CircuitRcd.HeightRequest = Math.Abs(CircuitRcd.HeightRequest) < 1 ? _circuitRcdHeight : 0;
        }

        private void TapGestureRecognizer1_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                RcdTypePicker.Focus();
            });
        }

        private void TapGestureRecognizer2_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                    RcdOpCurrentPicker.Focus();
            });
            }

        private void TapGestureRecognizer3_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                        CsaLivePicker.Focus();
            });
                }

        private void TapGestureRecognizer4_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CsaCpcPicker.Focus();
            });
        }
    }
}