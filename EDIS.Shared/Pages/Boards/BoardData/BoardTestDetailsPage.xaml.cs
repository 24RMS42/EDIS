using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Boards.BoardData
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardTestDetailsPage : ContentPage, IBoardData
	{
	    private double _zsAndIpfHeight = 180;
	    private double _testHeight = 520;
	    private double _testerAndDateHeight = 340;
	    private double _polarityHeight = 260;

        public BoardTestDetailsPage ()
		{
			InitializeComponent ();
		}

	    protected override void OnAppearing()
	    {
	        MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnBoardInfoTab, "Board Details - Board Test");
            base.OnAppearing();
	    }

	    private void TapGestureRecognizerx1_OnTapped(object sender, EventArgs e)
	    {
	        arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
	        ZsAndIpf.ScaleTo(Math.Abs(ZsAndIpf.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
	        ZsAndIpf.HeightRequest = Math.Abs(ZsAndIpf.HeightRequest) < 1 ? _zsAndIpfHeight : 0;
	    }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
	    {
	        arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
	        Test.ScaleTo(Math.Abs(Test.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
	        Test.HeightRequest = Math.Abs(Test.HeightRequest) < 1 ? _testHeight : 0;
	    }

	    private void TapGestureRecognizerx3_OnTapped(object sender, EventArgs e)
	    {
	        arrow3.RotateTo(arrow3.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
	        TesterAndDate.ScaleTo(Math.Abs(TesterAndDate.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
	        TesterAndDate.HeightRequest = Math.Abs(TesterAndDate.HeightRequest) < 1 ? _testerAndDateHeight : 0;
	    }

	    private void TapGestureRecognizerx4_OnTapped(object sender, EventArgs e)
	    {
	        arrow4.RotateTo(arrow4.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
	        PolarityPhaseVulnerability.ScaleTo(Math.Abs(PolarityPhaseVulnerability.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
	        PolarityPhaseVulnerability.HeightRequest = Math.Abs(PolarityPhaseVulnerability.HeightRequest) < 1 ? _polarityHeight : 0;
	    }
    }
}