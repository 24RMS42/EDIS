using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ElectricianInfoPage : ContentPage
	{
        private double _accreditionBlockHeight = 250;
        private double _testInstrumentSerialNumbersBlockHeight = 550;
        

        public ElectricianInfoPage ()
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
            arrow1.RotateTo(arrow1.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            AccreditionBlock.ScaleTo(Math.Abs(AccreditionBlock.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            AccreditionBlock.HeightRequest = Math.Abs(AccreditionBlock.HeightRequest) < 1 ? _accreditionBlockHeight : 0;
        }

        private void TapGestureRecognizerx2_OnTapped(object sender, EventArgs e)
        {
            arrow2.RotateTo(arrow2.Rotation == 180 ? 0 : 180, 250U, Easing.Linear);
            TestInstrumentSerialNumbersBlock.ScaleTo(Math.Abs(TestInstrumentSerialNumbersBlock.Scale) < 1 ? 1 : 0, 150U, Easing.CubicIn);
            TestInstrumentSerialNumbersBlock.HeightRequest = Math.Abs(TestInstrumentSerialNumbersBlock.HeightRequest) < 1 ? _testInstrumentSerialNumbersBlockHeight : 0;
        }
    }
}