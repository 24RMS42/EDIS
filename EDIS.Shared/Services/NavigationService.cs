using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EDIS.Shared.Extensions;
using EDIS.Shared.Pages.Base;
using EDIS.Shared.Pages.Boards;
using EDIS.Shared.Pages.Boards.BoardData;
using EDIS.Shared.Pages.Boards.BoardDetails;
using EDIS.Shared.Pages.Boards.Circuits;
using EDIS.Shared.Pages.Buildings;
using EDIS.Shared.Pages.Certificates;
using EDIS.Shared.Pages.Estates;
using EDIS.Shared.Pages.Login;
using EDIS.Shared.Pages.Misc;
using EDIS.Shared.Pages.Points;
using EDIS.Shared.Pages.Profile;
using EDIS.Shared.Pages.Settings;
using EDIS.Shared.Services.Interfaces;
using EDIS.Shared.ViewModels;
using EDIS.Shared.ViewModels.AppSettings;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards;
using EDIS.Shared.ViewModels.Boards.BoardData;
using EDIS.Shared.ViewModels.Boards.BoardDetails;
using EDIS.Shared.ViewModels.Buildings;
using EDIS.Shared.ViewModels.Certificates;
using EDIS.Shared.ViewModels.Circuits;
using EDIS.Shared.ViewModels.Estates;
using EDIS.Shared.ViewModels.Points;
using EDIS.Shared.ViewModels.Profile;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EDIS.Shared.Services
{
    public class NavigationService : INavigationService
    {
        protected Application CurrentApplication => Application.Current;
        protected readonly Dictionary<Type, Type> Mappings;
        protected readonly Dictionary<string, Type> Dialogs;

        public NavigationService()
        {
            Mappings = new Dictionary<Type, Type>();
            Dialogs = new Dictionary<string, Type>();

            CreatePageViewModelMappings();
            CreateDialogsMappings();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task ModalNavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalModalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateToRoot()
        {
            var mainPage = CurrentApplication.MainPage as MainPage;
            var popToRootAsync = mainPage?.Detail.Navigation.PopToRootAsync(true);
            if (popToRootAsync != null)
                await popToRootAsync;

            if (mainPage != null) mainPage.IsPresented = false;
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as MainPage;

            if (mainPage != null)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var stop = new Stopwatch();

            stop.Start();

            Page page = CreateAndBindPage(viewModelType, parameter);

            stop.Stop();

            Debug.WriteLine("bind " + stop.Elapsed);

            stop.Start();

            if (page is MainPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                mainPage.IsPresented = false;

                if (navigationPage != null)
                {
                    try
                    {
                        await navigationPage.PushAsync(page);

                        stop.Stop();

                        Debug.WriteLine("push " + stop.Elapsed);

                        stop.Start();
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            try
            {

                await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);

                stop.Stop();

                Debug.WriteLine("init " + stop.Elapsed);

                stop.Start();
            }
            catch (Exception e)
            {
                
            }
        }

        protected virtual async Task InternalModalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.Navigation.PushModalAsync(page);
                }

                mainPage.IsPresented = false;
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return Mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }
            try
            {
                var page = Activator.CreateInstance(pageType) as Page;
                var viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as BaseViewModel;
                page.BindViewModel(viewModel);

                if (page is IPageWithParameters)
                {
                    ((IPageWithParameters)page).InitializeWith(parameter);
                }

                return page;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void CreatePageViewModelMappings()
        {
            Mappings.Add(typeof(MainViewModel), typeof(MainPage));
            Mappings.Add(typeof(LoginViewModel), typeof(LoginPage));
            Mappings.Add(typeof(CertificatesViewModel), typeof(CertificatesPage));
            Mappings.Add(typeof(EstatesViewModel), typeof(EstatesPage));
            Mappings.Add(typeof(BuildingsViewModel), typeof(BuildingsPage));
            Mappings.Add(typeof(FilterCertificatesViewModel), typeof(FilterCertificatesPage));
            Mappings.Add(typeof(CertificateViewModel), typeof(CertificateDetailsPage));
            Mappings.Add(typeof(AddRemoveBoardsViewModel), typeof(AddRemoveBoardsPage));
            Mappings.Add(typeof(BoardViewModel), typeof(BoardDetailsPage));
            Mappings.Add(typeof(FilterBoardsViewModel), typeof(FilterBoardsPage));
            Mappings.Add(typeof(EditPointViewModel), typeof(EditPointPage));
            Mappings.Add(typeof(AllBoardsViewModel), typeof(AllBoardsPage));
            Mappings.Add(typeof(AllCertificatesViewModel), typeof(AllCertificatesPage));
            Mappings.Add(typeof(ProfileViewModel), typeof(ProfilePage));
            Mappings.Add(typeof(SettingsViewModel), typeof(SettingsPage));
            Mappings.Add(typeof(MainBoardDataViewModel), typeof(MainBoardDataPage));
            Mappings.Add(typeof(MainCircuitDataViewModel), typeof(MainCircuitDataPage));
            Mappings.Add(typeof(EditPointDetailsViewModel), typeof(EditPointDetailsPage));
        }

        private void CreateDialogsMappings()
        {

        }

        private void CreateMessengerSubscriptions() { }

        public async Task HideModalPage()
        {
            if (CurrentApplication.MainPage is MainPage)
            {
                var mainPage = CurrentApplication.MainPage as MainPage;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.Navigation.PopModalAsync(true);
                }
            }
        }
    }
}