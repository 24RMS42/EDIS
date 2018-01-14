using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using EDIS.Shared.Pages.Boards.BoardData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Boards.BoardDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicInfoPage : ContentPage, IBoardData, IBoardDetails
    {
        private double _informationHeight = 880;
        private double _boardLocationHeight = 260;
        private double _supplyToDBIsFromHeight = 530;
        private double _overcurrentProtectionDeviceHeight = 270;
        private double _associatedRCDHeight = 260;
        private double _otherInfoHeight = 260;
        private double _commentsHeight = 110;

        public BasicInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnBoardInfoTab, "Board Details - Board");
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnBoardAssociatedTab, "Board Details - Basic Info");
            base.OnAppearing();
        }

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            Information.ScaleTo(Math.Abs(Information.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            Information.HeightRequest = Math.Abs(Information.HeightRequest) < 1 ? _informationHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            BoardLocation.ScaleTo(Math.Abs(BoardLocation.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            BoardLocation.HeightRequest = Math.Abs(BoardLocation.HeightRequest) < 1 ? _boardLocationHeight : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            SupplyToDBIsFrom.ScaleTo(Math.Abs(SupplyToDBIsFrom.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            SupplyToDBIsFrom.HeightRequest = Math.Abs(SupplyToDBIsFrom.HeightRequest) < 1 ? _supplyToDBIsFromHeight : 0;
        }

        private void TapGestureRecognizerx4_OnTapped(object sender, EventArgs e)
        {
            arrow4.RotateTo(arrow4.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            OvercurrentProtectionDevice.ScaleTo(Math.Abs(OvercurrentProtectionDevice.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            OvercurrentProtectionDevice.HeightRequest = Math.Abs(OvercurrentProtectionDevice.HeightRequest) < 1 ? _overcurrentProtectionDeviceHeight : 0;
        }

        private void TapGestureRecognizerx5_OnTapped(object sender, EventArgs e)
        {
            arrow5.RotateTo(arrow5.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            AssociatedRCD.ScaleTo(Math.Abs(AssociatedRCD.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            AssociatedRCD.HeightRequest = Math.Abs(AssociatedRCD.HeightRequest) < 1 ? _associatedRCDHeight : 0;
        }

        private void TapGestureRecognizerx6_OnTapped(object sender, EventArgs e)
        {
            arrow6.RotateTo(arrow6.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            OtherInfo.ScaleTo(Math.Abs(OtherInfo.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            OtherInfo.HeightRequest = Math.Abs(OtherInfo.HeightRequest) < 1 ? _otherInfoHeight : 0;
        }

        private void TapGestureRecognizerx7_OnTapped(object sender, EventArgs e)
        {
            arrow7.RotateTo(arrow7.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            Comments.ScaleTo(Math.Abs(Comments.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            Comments.HeightRequest = Math.Abs(Comments.HeightRequest) < 1 ? _commentsHeight : 0;
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