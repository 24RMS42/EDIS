using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Points
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPointDetailsPage : ContentPage
    {
        private double _basicInfoHeight = 180;
        private double _rcd = 600;
        private double _zpo = 420;

        public EditPointDetailsPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            BasicInfo.ScaleTo(Math.Abs(BasicInfo.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            BasicInfo.HeightRequest = Math.Abs(BasicInfo.HeightRequest) < 1 ? _basicInfoHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            RcdDetails.ScaleTo(Math.Abs(RcdDetails.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            RcdDetails.HeightRequest = Math.Abs(RcdDetails.HeightRequest) < 1 ? _rcd : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            ZPO.ScaleTo(Math.Abs(ZPO.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            ZPO.HeightRequest = Math.Abs(ZPO.HeightRequest) < 1 ? _zpo : 0;
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
    }
}