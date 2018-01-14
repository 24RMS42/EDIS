using EDIS.Service.Interfaces;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using EDIS.Domain.Profile;

namespace EDIS.Shared.ViewModels.Profile
{
    public class ElectricianInfoViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private User _user;

        public User User
        {
            get => _user;
            set { Set(() => User, ref _user, value); }
        }

        private Instrument _instrument;

        public Instrument Instrument
        {
            get => _instrument;
            set { Set(() => Instrument, ref _instrument, value); }
        }
        private RelayCommand _saveUserElectricianInfoCommand;

        public RelayCommand SaveUserElectricianInfoCommand
        {
            get
            {
                return _saveUserElectricianInfoCommand ?? (_saveUserElectricianInfoCommand = new RelayCommand(() =>
                {
                    _profileService.SaveUserElectricianInfo(User, Instrument);
                }));
            }
        }

        public ElectricianInfoViewModel(IProfileService profileService)
        {
            Title = "Electrian Info";

            _profileService = profileService;
            User = new User();
            Instrument = new Instrument();
        }
    }
}
