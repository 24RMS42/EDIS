using EDIS.Shared.Pages.Base;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Helpers;
using Xamarin.Forms;
using BindingMode = Xamarin.Forms.BindingMode;

namespace EDIS.Shared.Extensions
{
    public static class PageExtensions
    {
        public static void BindViewModel(this Page page, BaseViewModel viewModel)
        {
            page.BindingContext = viewModel;

            page.Appearing += async (sender, args) => await viewModel.OnAppearing();
            page.Disappearing += async (sender, args) => await viewModel.OnDisappearing();

            page.SetBinding(Page.TitleProperty, "Title", BindingMode.TwoWay);
        }
    }
}