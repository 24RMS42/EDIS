using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EDIS.Core;
using EDIS.Core.Exceptions;
using EDIS.Domain.Profile;
using EDIS.Service.Interfaces;
using EDIS.Shared.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace EDIS.Shared.ViewModels.Base
{
    public class BaseViewModel : ViewModelBase
    {
        protected readonly INavigationService NavigationService;
        protected IUserDialogs Dialogs { get; }
        private IAuthenticationService _authenticationService;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(() => IsBusy, ref _isBusy, value); }
        }

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        public string Icon { get; set; }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public static string CurUserId
        {
            get => Settings.UserId;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Settings.UserId = value;
            }
        }


        public static string EstateId
        {
            get => Settings.EstateId;
            set
            {
                if(!string.IsNullOrEmpty(value))
                    Settings.EstateId = value;
            }
        }

        public static string BuildingId
        {
            get => Settings.BuildingId;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Settings.BuildingId = value;
            }
        }

        public static string CertificateId { get; set; }

        public virtual async Task OnAppearing()
        {
            await Task.FromResult(true);
        }

        public virtual async Task OnDisappearing()
        {
            await Task.FromResult(true);
        }

        public virtual void OnBindingContextChanged()
        {

        }

        public async Task<bool> TryToLogin()
        {
            try
            {
                var username = Settings.Username;
                var password = Settings.Password;
                var rememberMe = Settings.RememberMe;

                if (!rememberMe)
                    return false;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return false;

                await _authenticationService.LoginAsync(username, password, true);

                /*User user = await _profileService.GetProfile();
                if (user != null && user.UserId != null && !user.UserId.Equals(""))
                {
                    CurUserId = user.UserId;
                }
                else
                {
                    //this.Dialogs.ShowError("Sorry, could not receive Master data!");
                    //Dialogs.Toast("Sorry, could not receive User profile data!");
                    CurUserId = "";
                }*/

                return true;
            }
            catch (ServiceAuthenticationException e)
            {
                Dialogs.ShowError(e.Content);
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }

            return false;
        }

        public async Task ShowErrorAlert(string message)
        {
            await Dialogs.AlertAsync(message, "Error");
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { Set(() => User, ref _user, value); }
        }

        public BaseViewModel()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            Dialogs = UserDialogs.Instance;
            _authenticationService = ViewModelLocator.Instance.Resolve<IAuthenticationService>();
            User = new User();
        }
    }
}