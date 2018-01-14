using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIS.Shared.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDIS.Shared.Pages.Boards.BoardDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircuitsPage : ContentPage, IBoardDetails
    {
        public CircuitsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.ChangeTitleOnBoardAssociatedTab, "Board Details - Circuit");
            base.OnAppearing();
        }
    }
}