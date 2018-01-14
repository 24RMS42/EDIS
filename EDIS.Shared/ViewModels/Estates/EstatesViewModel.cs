using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Estates;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Buildings;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Estates
{
    public class EstatesViewModel : BaseViewModel
    {
        private readonly IEstatesService _estatesService;
        private readonly ILookupsService _lookupsService;
        private readonly IEstatesLookupsService _estatesLookupsService;

        public ObservableCollectionFast<EstateRow> Estates { get; set; }

        private RelayCommand<EstateRow> _estateSelectedCommand;

        public RelayCommand<EstateRow> EstateSelectedCommand
        {
            get
            {
                return _estateSelectedCommand ?? (_estateSelectedCommand = new RelayCommand<EstateRow>(async estate =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(100);

                        if (estate != null)
                            EstateId = estate.EstateId;

                        await NavigationService.NavigateToAsync<BuildingsViewModel>();
                    }
                }));
            }
        }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(async () =>
                {
                    await GetEstates(true);

                    await UpdateMasterData();
                }));
            }
        }

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

        public EstatesViewModel(IEstatesService estatesService, ILookupsService lookupsService, IEstatesLookupsService estatesLookupsService)
        {
            Title = "Estates";

            _estatesService = estatesService;
            _lookupsService = lookupsService;
            _estatesLookupsService = estatesLookupsService;

            Estates = new ObservableCollectionFast<EstateRow>();
        }

        public override async Task InitializeAsync(object navigationData)
        {          
             await GetEstates(true);  
        }

        private async Task GetEstates(bool forceCloud = false)
        {
            Estates.Clear();

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var result = await _estatesService.GetAllEstates(forceCloud);
                    if (!result.Success)
                    {
                        Dialogs.Toast(result.Message);
                        return;
                    }

                    var estates = result.ResultObject as IEnumerable<EstateRow>;

                    if (estates != null && estates.Any())
                        Estates.AddRange(estates);
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await GetEstates(forceCloud);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }
    }
}