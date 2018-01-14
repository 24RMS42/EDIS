using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Buildings;
using EDIS.Shared.ViewModels.Certificates;
using EDIS.Shared.ViewModels.Estates;
using EDIS.Shared.ViewModels.Profile;
using EDIS.Service.Interfaces;
using GalaSoft.MvvmLight.Command;
using EDIS.Domain.Profile;
using System;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Core.Exceptions;
using EDIS.Shared.ViewModels.AppSettings;
using Version.Plugin;
using EDIS.Service;
using Xamarin.Forms;
using EDIS.Shared.Helpers;

namespace EDIS.Shared.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly ILookupsService _lookupsService;
        private readonly IProfileService _profileService;
        private string _response = string.Empty;

        public MenuViewModel(ILookupsService lookupsService, IProfileService profileService)
        {
            _lookupsService = lookupsService;
            _profileService = profileService;
        }

        private string _apiEndPoint;

        public string ApiEndPoint
        {
            get { return _apiEndPoint; }
            set { Set(() => ApiEndPoint, ref _apiEndPoint, value); }
        }

        private string _appVersion;

        public string AppVersion
        {
            get { return _appVersion; }
            set { Set(() => AppVersion, ref _appVersion, value); }
        }

        private RelayCommand _openEstatesPage;

        public RelayCommand OpenEstatesPage
        {
            get
            {
                return _openEstatesPage ?? (_openEstatesPage = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<EstatesViewModel>();
                    }
                }));
            }
        }

        private RelayCommand _openBuildingsPage;

        public RelayCommand OpenBuildingsPage
        {
            get
            {
                return _openBuildingsPage ?? (_openBuildingsPage = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<BuildingsViewModel>();
                    }
                }));
            }
        }

        private RelayCommand _openCertificatesPage;

        public RelayCommand OpenCertificatesPage
        {
            get
            {
                return _openCertificatesPage ?? (_openCertificatesPage = new RelayCommand(async () =>
                {
                    await NavigationService.NavigateToRoot();
                }));
            }
        }

        private RelayCommand _openLoginPage;

        public RelayCommand OpenLoginPage
        {
            get
            {
                return _openLoginPage ?? (_openLoginPage = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<LoginViewModel>();
                    }
                }));
            }
        }

        private RelayCommand _userDataCommand;

        public RelayCommand UserDataCommand => _userDataCommand ?? (_userDataCommand = new RelayCommand(async () =>
        {
            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    User user = await _profileService.GetProfile("");
                    if (user != null && user.UserId != null && !user.UserId.Equals(""))
                    {
                        //User = user.UserName;
                        Dialogs.Toast("User profile has been updated.");
                    }
                    else
                    {
                        //this.Dialogs.ShowError("Sorry, could not receive Master data!");
                        Dialogs.Toast("Sorry, could not receive User profile data!");
                    }
                }
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }

        }));

        private RelayCommand _openSettingsPage;

        public RelayCommand OpenSettingsPage
        {
            get
            {
                return _openSettingsPage ?? (_openSettingsPage = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<SettingsViewModel>();
                    }
                }));
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var user = await _profileService.GetProfile(Settings.Username);
                if (user != null)
                    User = user;

                ApiEndPoint = string.IsNullOrEmpty(Settings.Api) ? GlobalSettings.BaseURL : Settings.Api;
                AppVersion = CrossVersion.Current.Version;

               
            }
            catch (ServiceAuthenticationException e)
            {
                //var result = await TryToLogin();
                //if (!result)
                //    await NavigationService.NavigateToAsync<LoginViewModel>();
                //else
                //    await InitializeAsync(navigationData);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }
		
		private RelayCommand _openProfilePage;

        public RelayCommand OpenProfilePage
        {
            get
            {
                return _openProfilePage ?? (_openProfilePage = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(200);
                        await NavigationService.NavigateToAsync<ProfileViewModel>();
                    }
                }));
            }
        }

        public async Task Refresh()
        {
            try
            {
                var user = await _profileService?.GetProfile(Settings.Username);
                if (user != null)
                    User = user;
            }
            catch (Exception e)
            {
                
            }
            

            ApiEndPoint = string.IsNullOrEmpty(Settings.Api) ? GlobalSettings.BaseURL : Settings.Api;
            AppVersion = CrossVersion.Current.Version;

            RaisePropertyChanged(() => ApiEndPoint);
            RaisePropertyChanged(() => AppVersion);
            RaisePropertyChanged(() => User);
        }

        public MenuViewModel(IProfileService profileService)
        {
            Title = "Menu";

            _profileService = profileService;

            MessagingCenter.Subscribe<SettingsViewModel>(this, MessengerCenterMessages.RefreshApiOnMenu, (model) =>
            {
                ApiEndPoint = Settings.Api;

                Refresh();
            });

            MessagingCenter.Subscribe<CertificatesViewModel>(this, MessengerCenterMessages.RefreshApiOnMenu, (model) =>
            {
                Refresh();
            });
        }
    }
}