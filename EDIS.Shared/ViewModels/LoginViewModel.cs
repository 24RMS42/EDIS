using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Service.Interfaces;
using EDIS.Shared.ViewModels.Base;
using EDIS.Domain.Profile;
using GalaSoft.MvvmLight.Command;
using EDIS.Core;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileService _profileService;
        private readonly ILookupsService _lookupsService;
        private readonly IEstatesLookupsService _estatesLookupsService;

        private string _username;

        public string Username
        {
            get { return _username; }
            set { Set(() => Username, ref _username, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { Set(() => Password, ref _password, value); }
        }

        private bool _rememberMe = Settings.RememberMe;

        public bool RememberMe
        {
            get { return _rememberMe; }
            set { Set(() => RememberMe, ref _rememberMe, value); }
        }

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(async () =>
        {
            await OnLoginCommand();
        }));

        private async Task OnLoginCommand()
        {
            try
            {
                var response = await _authenticationService.LoginAsync(Username, Password, RememberMe);
                Settings.RememberMe = RememberMe;

                Dialogs.Toast("Successful");

                await UpdateMasterData();

                User user = await _profileService.GetProfile("");
                if (user != null && user.UserId != null && !user.UserId.Equals(""))
                {
                    CurUserId = user.UserId;
                }
                else
                {
                    //this.Dialogs.ShowError("Sorry, could not receive Master data!");
                    //Dialogs.Toast("Sorry, could not receive User profile data!");
                    CurUserId = "";
                }

                await NavigationService.NavigateBackAsync();
            }
            catch (ServiceAuthenticationException e)
            {
                await ShowErrorAlert(e.Content);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }


        private RelayCommand _forgotPasswordCommand;

        public RelayCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new RelayCommand(() =>
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Xamarin.Forms.Device.OpenUri(new Uri(Settings.WebAppBaseURL + "/forgot-password"));
                });
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }

        }));

        private async Task UpdateMasterData()
        {
            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    string res = await _lookupsService.FetchLookups(true);
                    if (res != null && res.Equals("SUCCESS"))
                    {
                        //this.Dialogs.Alert("Master data has been updated.");
                        Dialogs.Alert("Master data has been updated.");
                    }
                    else
                    {
                        //this.Dialogs.ShowError("Sorry, could not receive Master data!");
                        Dialogs.ShowError("Sorry, could not receive Master data!");
                    }

                    if (!string.IsNullOrEmpty(EstateId))
                    {
                        var stat = await _estatesLookupsService.FetchEstateLookups(EstateId, true);

                        if (stat != null && stat.Equals("SUCCESS"))
                        {
                            Dialogs.Alert("Estate background data has been updated");
                        }
                        else
                        {
                            Dialogs.ShowError("Sorry, could not receive data!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        public override async Task OnAppearing()
        {
            if (RememberMe)
            {
                Username = Settings.Username;
                Password = Settings.Password;
            }

            await Task.FromResult(true);
        }

        public LoginViewModel(IAuthenticationService authenticationService, IProfileService profileService, ILookupsService lookupsService, IEstatesLookupsService estatesLookupsService)
        {
            Title = "Login";

            _authenticationService = authenticationService;
            _profileService = profileService;
            _lookupsService = lookupsService;
            _estatesLookupsService = estatesLookupsService;
        }
    }
}