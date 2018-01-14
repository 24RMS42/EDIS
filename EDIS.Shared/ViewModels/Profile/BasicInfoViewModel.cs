using EDIS.Service.Interfaces;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using EDIS.Domain.Profile;
using System.ComponentModel;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Profile
{
    public class BasicInfoViewModel : BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        private IProfileService _profileService;
        private User _user;

        public User User
        {
            get => _user;
            set { Set(() => User, ref _user, value); }
        }

        private string _userLogoPath;

        public string UserLogoPath
        {
            get
            {
                //if (!string.IsNullOrEmpty(_userLogoPath))
                //{
                //    return "{local: ImageResource " + _userLogoPath + "}";
                //}
                //return "";
                return _userLogoPath;
            }
            set{
                //_userLogoPath = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("UserLogoPath"));

                if (_userLogoPath != value)
                {
                    _userLogoPath = value;

                    //if (PropertyChanged != null)
                    //{
                        //PropertyChanged(this,
                            //new PropertyChangedEventArgs("UserLogoPath"));
                    //}
                }
            }

        }

        //Set(() => UserLogoPath, ref _userLogoPath, value); }

        private RelayCommand _saveUserBasicInfoCommand;

        public RelayCommand SaveUserBasicInfoCommand
        {
            get
            {
                return _saveUserBasicInfoCommand ?? (_saveUserBasicInfoCommand = new RelayCommand(() =>
                {
                    _profileService.SaveUserBasicInfo(User);
                }));
            }
        }
                
        public BasicInfoViewModel(IProfileService profileService)
        {
            Title = "Basic info";
            //IProfileService profileService
            _profileService = profileService;
            User = new User();
            UserLogoPath = "";
        }
    }
}
