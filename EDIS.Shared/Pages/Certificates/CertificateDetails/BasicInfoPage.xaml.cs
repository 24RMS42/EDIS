using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Certificates.CertificateDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicInfoPage : ContentPage, ICertificateDetails
    {
        private double _detailsOfTheClientHeight = 180;
        private double _purposeOfTheReportHeight = 210;
        private double _detailsOfInstallationHeight = 950;
        private double _associatedContractorAndSupervisorHeight = 180;
        private double _extentAndLimitationsOfInspectionHeight = 450;

        public BasicInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, "Certificate Details - Basic Info");
            base.OnAppearing();
        }

        private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
        {
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            DetailsOfTheClient.ScaleTo(Math.Abs(DetailsOfTheClient.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            DetailsOfTheClient.HeightRequest = Math.Abs(DetailsOfTheClient.HeightRequest) < 1 ? _detailsOfTheClientHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            PurposeOfTheReport.ScaleTo(Math.Abs(PurposeOfTheReport.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            PurposeOfTheReport.HeightRequest = Math.Abs(PurposeOfTheReport.HeightRequest) < 1 ? _purposeOfTheReportHeight : 0;
        }

        private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
        {
            arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            DetailsOfInstallation.ScaleTo(Math.Abs(DetailsOfInstallation.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            DetailsOfInstallation.HeightRequest = Math.Abs(DetailsOfInstallation.HeightRequest) < 1 ? _detailsOfInstallationHeight : 0;
        }

        private void TapGestureRecognizerx4_OnTapped(object sender, EventArgs e)
        {
            arrow4.RotateTo(arrow4.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            AssociatedContractorAndSupervisor.ScaleTo(Math.Abs(AssociatedContractorAndSupervisor.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            AssociatedContractorAndSupervisor.HeightRequest = Math.Abs(AssociatedContractorAndSupervisor.HeightRequest) < 1 ? _associatedContractorAndSupervisorHeight : 0;
        }

        private void TapGestureRecognizerx5_OnTapped(object sender, EventArgs e)
        {
            arrow5.RotateTo(arrow5.Rotation == 180 ? 0 : 180, 150U, Easing.Linear);
            ExtentAndLimitationsOfInspection.ScaleTo(Math.Abs(ExtentAndLimitationsOfInspection.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            ExtentAndLimitationsOfInspection.HeightRequest = Math.Abs(ExtentAndLimitationsOfInspection.HeightRequest) < 1 ? _extentAndLimitationsOfInspectionHeight : 0;
        }

        private void TapGestureRecognizer10_OnTapped(object sender, EventArgs e)
        {
            //ResistancesPicker10.Focus();
        }
    }
}