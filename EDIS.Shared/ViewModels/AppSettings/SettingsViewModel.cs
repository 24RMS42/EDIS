using System.Collections.Generic;
using System.Linq;
using EDIS.Shared.ViewModels.Base;
using Xamarin.Forms;
using EDIS.Shared.Helpers;
using GalaSoft.MvvmLight.Command;
using System;
using EDIS.Service;
using EDIS.Service.Interfaces;

namespace EDIS.Shared.ViewModels.AppSettings
{
    public sealed class SettingsViewModel : BaseViewModel
    {
        private readonly ILookupsService _lookupsService;
        private readonly IEstatesLookupsService _estatesLookupsService;

        private List<string> _apis;

        public List<string> Apis
        {
            get { return _apis; }
            set { Set(() => Apis, ref _apis, value); }
        }

        private string _selectedApi;

        public string SelectedApi
        {
            get { return _selectedApi; }
            set
            {
                Set(() => SelectedApi, ref _selectedApi, value);
                if (string.IsNullOrEmpty(value))
                    return;

                Core.Settings.Api = value;

                MessagingCenter.Send(this, MessengerCenterMessages.RefreshApiOnMenu);
            }
        }

        private RelayCommand _updateMasterData;

        public RelayCommand UpdateMasterData
        {
            get
            {
                return _updateMasterData ?? (_updateMasterData = new RelayCommand(async () =>
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
                }));
            }
        }

        public SettingsViewModel(ILookupsService lookupsService, IEstatesLookupsService estatesLookupsService)
        {
            _lookupsService = lookupsService;
            _estatesLookupsService = estatesLookupsService;

            Title = "Settings";

            Apis = new List<string>
            {
                "http://api-dev.electricalcertificates.co.uk",
                "http://api-stage.electricalcertificates.co.uk",
                "https://api.electricalcertificates.co.uk"
            };

            var api = Core.Settings.Api;
            if (string.IsNullOrEmpty(api))
            {
                api = GlobalSettings.BaseURL;
                Core.Settings.Api = api;
            }
            SelectedApi = api;
        }
    }
}