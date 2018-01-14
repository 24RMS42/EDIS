using EDIS.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace EDIS.iOS.Renderer
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Set Text Font for unselected tab states
            UITextAttributes normalTextAttributes = new UITextAttributes();
            normalTextAttributes.Font = UIFont.SystemFontOfSize(17.0F); // unselected

            UITabBarItem.Appearance.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
        }

        public override UIViewController SelectedViewController
        {
            get
            {
                UITextAttributes selectedTextAttributes = new UITextAttributes();
                selectedTextAttributes.Font = UIFont.SystemFontOfSize(20.0F); // SELECTED
                if (base.SelectedViewController != null)
                {
                    base.SelectedViewController.TabBarItem.SetTitleTextAttributes(selectedTextAttributes, UIControlState.Normal);
                }
                return base.SelectedViewController;
            }
            set
            {
                base.SelectedViewController = value;

                foreach (UIViewController viewController in base.ViewControllers)
                {
                    UITextAttributes normalTextAttributes = new UITextAttributes();
                    normalTextAttributes.Font = UIFont.SystemFontOfSize(17.0F); // unselected

                    viewController.TabBarItem.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
                }
            }
        }
    }
}