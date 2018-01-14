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
    public partial class CircuitTestDetailsPage : ContentPage, ICircuit
    {
        private double _circuitImpedanceRingHeight = 260;
        private double _circuitImpedanceAllHeight = 200;
        private double _insulationResistanceHeight = 200;
        private double _polarityHeight = 200;
        private double _rcdHeight = 270;
        private double _circuitObservationsHeight = 270;

        public CircuitTestDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnCircuitTab, "Circuit Details - Circuit Test");
            base.OnAppearing();
        }

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            CircuitImpedanceRing.ScaleTo(Math.Abs(CircuitImpedanceRing.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            CircuitImpedanceRing.HeightRequest = Math.Abs(CircuitImpedanceRing.HeightRequest) < 1 ? _circuitImpedanceRingHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            CircuitImpedanceAll.ScaleTo(Math.Abs(CircuitImpedanceAll.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            CircuitImpedanceAll.HeightRequest = Math.Abs(CircuitImpedanceAll.HeightRequest) < 1 ? _circuitImpedanceAllHeight : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            InsulationResistance.ScaleTo(Math.Abs(InsulationResistance.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            InsulationResistance.HeightRequest = Math.Abs(InsulationResistance.HeightRequest) < 1 ? _insulationResistanceHeight : 0;
        }

        private void TapGestureRecognizerx4_OnTapped(object sender, EventArgs e)
        {
            arrow4.RotateTo(arrow4.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            PolarityAndZs.ScaleTo(Math.Abs(PolarityAndZs.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            PolarityAndZs.HeightRequest = Math.Abs(PolarityAndZs.HeightRequest) < 1 ? _polarityHeight : 0;
        }

        private void TapGestureRecognizerx5_OnTapped(object sender, EventArgs e)
        {
            arrow5.RotateTo(arrow5.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            RCDMeasurements.ScaleTo(Math.Abs(RCDMeasurements.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            RCDMeasurements.HeightRequest = Math.Abs(RCDMeasurements.HeightRequest) < 1 ? _rcdHeight : 0;
        }

        private void TapGestureRecognizerx6_OnTapped(object sender, EventArgs e)
        {
            arrow6.RotateTo(arrow6.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            CircuitObservations.ScaleTo(Math.Abs(CircuitObservations.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            CircuitObservations.HeightRequest = Math.Abs(CircuitObservations.HeightRequest) < 1 ? _circuitObservationsHeight : 0;
        }

        private void TapGestureRecognizer1_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ResistancesPicker1.Focus();
            });
        }

        private void TapGestureRecognizer2_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ResistancesPicker2.Focus();
            });
        }
    }
}