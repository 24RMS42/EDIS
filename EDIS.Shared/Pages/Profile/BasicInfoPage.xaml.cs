using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using EDIS.Shared.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasicInfoPage : ContentPage
	{
        private double _generalBlockHeight = 790;
        private double _logoForCertificatesBlockHeight = 140;
        private double _declaractionBlockHeight = 140;

        public BasicInfoPage()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {

            }
		}

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            GeneralBlock.ScaleTo(Math.Abs(GeneralBlock.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            GeneralBlock.HeightRequest = Math.Abs(GeneralBlock.HeightRequest) < 1 ? _generalBlockHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            LogoForCertificatesBlock.ScaleTo(Math.Abs(LogoForCertificatesBlock.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            LogoForCertificatesBlock.HeightRequest = Math.Abs(LogoForCertificatesBlock.HeightRequest) < 1 ? _logoForCertificatesBlockHeight : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            DeclaractionBlock.ScaleTo(Math.Abs(DeclaractionBlock.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            DeclaractionBlock.HeightRequest = Math.Abs(DeclaractionBlock.HeightRequest) < 1 ? _declaractionBlockHeight : 0;
        }
    }
}