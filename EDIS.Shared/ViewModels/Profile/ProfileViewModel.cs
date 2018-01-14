using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Profile;
using EDIS.Service.Interfaces;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using EDIS.Core;
using EDIS.Shared.Helpers;

namespace EDIS.Shared.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _userEmail;
        private string _userId;
        private readonly IProfileService _profileService;

        public BasicInfoViewModel BasicInfoViewModel { get; set; }
        public ElectricianInfoViewModel ElectricianInfoViewModel { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            //var certId = navigationData as string;
            //var userEmail = Settings.Username as string;
            //if (userEmail != null) {
            //    _userEmail = userEmail;
            //}
            _userId = CurUserId;

            if (string.IsNullOrEmpty(CurUserId))
            {
                Dialogs.Toast("Plase login first");
                return;
            }

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var userResult = await _profileService.GetProfileDetails(_userId);
                    if (!userResult.Success)
                    {
                        Dialogs.Toast(userResult.Message);
                        return;
                    }

                    var userProfile = userResult.ResultObject as EditProfile;

                    if (userProfile != null)
                    {
                        

                        BasicInfoViewModel.User = userProfile.User;
                        BasicInfoViewModel.UserLogoPath = userProfile.UserLogoPath;
                        ElectricianInfoViewModel.User = userProfile.User;
                        ElectricianInfoViewModel.Instrument = userProfile.Instrument;

                        //foreach (var board in cert.CertificateAssociatedBoards.BoardTests)
                        //{
                        //    await _boardsService.GetBoardDetails(board.BoardId, BuildingId);
                        //}
                    }

                    
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await InitializeAsync(navigationData);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public override async Task OnAppearing()
        {
            await ElectricianInfoViewModel.OnAppearing();
        }

        public ProfileViewModel(IProfileService profileService)
        {
            Title = "Profile";

            _profileService = profileService;

            BasicInfoViewModel = new BasicInfoViewModel(profileService);
            ElectricianInfoViewModel = new ElectricianInfoViewModel(profileService);
        }
    }
}