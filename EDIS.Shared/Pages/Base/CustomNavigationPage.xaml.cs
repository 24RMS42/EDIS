using Xamarin.Forms;

namespace EDIS.Shared.Pages.Base
{
    public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage() : base()
        {
            InitializeComponent();
            BarTextColor = Color.Black;
            BarBackgroundColor = Color.FromHex("cccccc");
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
            BarTextColor = Color.Black;
            BarBackgroundColor = Color.FromHex("cccccc");
        }
    }
}