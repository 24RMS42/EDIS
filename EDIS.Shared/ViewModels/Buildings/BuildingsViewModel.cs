using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Core.Exceptions;
using EDIS.Domain.Buildings;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Certificates;
using GalaSoft.MvvmLight.Command;

namespace EDIS.Shared.ViewModels.Buildings
{
    public class BuildingsViewModel : BaseViewModel
    {
        private readonly IBuildingsService _buildingsService;
        private readonly IBuildingService _buildingService;

        public ObservableCollectionFast<BuildingRow> Buildings { get; set; }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(async () =>
                {
                    await GetBuildings(forceCloud: true);
                }));
            }
        }

        private RelayCommand<BuildingRow> _buildingSelectedCommand;

        public RelayCommand<BuildingRow> BuildingSelectedCommand
        {
            get
            {
                return _buildingSelectedCommand ?? (_buildingSelectedCommand = new RelayCommand<BuildingRow>(async building =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        await Task.Delay(100);

                        if (building != null)
                        {
                            Settings.Privileges = building.BuildingPrivileges;
                            BuildingId = building.BuildingId;
                            await FetchBuldingDetail(BuildingId, true);
                        }


                        await NavigationService.NavigateToRoot();
                    }
                }));
            }
        }

        public BuildingsViewModel(IBuildingsService buildingsService, IBuildingService buildingService)
        {
            Title = "Buildings";

            _buildingsService = buildingsService;
            _buildingService = buildingService;

            Buildings = new ObservableCollectionFast<BuildingRow>();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await GetBuildings(true);
            
        }

        private async Task GetBuildings(bool forceCloud = false)
        {
            using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
            {
                Buildings.Clear();

                try
                {
                    var result = await _buildingsService.GetAllBuildings(EstateId, forceCloud);
                    if (!result.Success)
                    {
                        Dialogs.Toast(result.Message);
                        return;
                    }

                    var buildings = result.ResultObject as IEnumerable<BuildingRow>;

                    if (buildings != null && buildings.Any())
                        Buildings.AddRange(buildings);
                }
                catch (ServiceAuthenticationException e)
                {
                    var result = await TryToLogin();
                    if (!result)
                        await NavigationService.NavigateToAsync<LoginViewModel>();
                    else
                        await GetBuildings(forceCloud);
                }
                catch (Exception e)
                {
                    await ShowErrorAlert(e.Message);
                }
            }
        }

        private async Task FetchBuldingDetail(string buildingId, bool forceCloud = false)
        {
            //Buildings.Clear();

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var stat = await _buildingService.FetchBuldingDetail(buildingId, forceCloud);

                    if (stat != null && stat.Equals("SUCCESS"))
                    {
                        //Dialogs.Alert("Building Users data has been updated");
                    }
                    else
                    {
                        Dialogs.ShowError("Sorry, could not receive building data!");
                    }
                }
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }


        }

    }
}