using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Certificates;
using EDIS.Domain.Lookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.Pages.Certificates.CertificateDetails;
using EDIS.Shared.Pages.Points;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Points;
using Xamarin.Forms;
using System.Collections.Generic;
using EDIS.Domain.Buildings;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class CertificateViewModel : BaseViewModel
    {
        private string _certId;
        private readonly ICertificatesService _certificatesService;
        private readonly IBoardsService _boardsService;
        private IBuildingService _buildingService;
        private ILookupsService _lookupsService;

        private List<BuildingUser> _contractors;
        public List<BuildingUser> Contractors
        {
            get { return _contractors; }
            set { Set(() => Contractors, ref _contractors, value); }
        }

        private List<BuildingUser> _supervisors;
        public List<BuildingUser> Supervisors
        {
            get { return _supervisors; }
            set { Set(() => Supervisors, ref _supervisors, value); }
        }

        public BasicInfoViewModel BasicInfoViewModel { get; set; }
        public AssociatedBoardsViewModel AssociatedBoardsViewModel { get; set; }
        public EditPointViewModel EditPointViewModel { get; set; }



        public override async Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<BasicInfoPage, string>(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, (details, s) =>
            {
                Title = s;
            });
            MessagingCenter.Subscribe<AssociatedBoardsPage, string>(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, (details, s) =>
            {
                Title = s;
            });
            MessagingCenter.Subscribe<EditPointPage, string>(this, MessengerCenterMessages.ChangeTitleOnCertificateTab, (details, s) =>
            {
                Title = s;
            });

            var certId = navigationData as string;
            if (certId != null)
                _certId = certId;

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var certResult = await _certificatesService.GetCertificateDetails(BuildingId, _certId);
                    if (!certResult.Success)
                    {
                        Dialogs.Toast(certResult.Message);
                        return;
                    }

                    var cert = certResult.ResultObject as EditCertificate;

                    if (cert != null)
                    {
                        await loadBuildingUsers();

                        var amendDates = await _lookupsService.GetBsAmendmentDates(false);
                        List<BsAmendmentDates> bsAmendmentDates = (List<BsAmendmentDates>)amendDates;

                        BasicInfoViewModel.Certificate = cert.CertificateBasicInfo;
                        BasicInfoViewModel.Contractors = Contractors;
                        BasicInfoViewModel.SelectedContractor = Contractors.FirstOrDefault(x => x.UserId == cert.CertificateBasicInfo.ConUserId);
                        //BasicInfoViewModel.RaisePropertyChanged(() => SelectedContractor);

                        BasicInfoViewModel.Supervisors = Supervisors;
                        BasicInfoViewModel.SelectedSupervisor = Supervisors.FirstOrDefault(x => x.UserId == cert.CertificateBasicInfo.EsUserId);
                        //BasicInfoViewModel.RaisePropertyChanged(() => SelectedSupervisor);

                        BasicInfoViewModel.BsAmendmentDates = bsAmendmentDates;
                        BasicInfoViewModel.SelectedBsAmendmentDate = bsAmendmentDates.FirstOrDefault(x => x.CertDateAmended == cert.CertificateBasicInfo.CertDateAmended);
                        //BasicInfoViewModel.RaisePropertyChanged(() => SelectedBsAmendmentDate);

                        AssociatedBoardsViewModel.AssociatedBoards.AddRange(cert.CertificateAssociatedBoards
                            .BoardTests);

                        foreach (var board in cert.CertificateAssociatedBoards.BoardTests)
                        {
                            await _boardsService.GetBoardDetails(board.BoardId, BuildingId);
                        }
                    }

                    await EditPointViewModel.InitializeAsync(null);
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
            await AssociatedBoardsViewModel.OnAppearing();
            await EditPointViewModel.OnAppearing();
        }

        private async Task loadBuildingUsers()
        {
            try
            {
                var bUsers = await _buildingService.GetBuildingUsers(BuildingId, false);
                List<BuildingUser> users = (List<BuildingUser>)bUsers;
                if (users != null && users.Count > 0)
                {
                    for (var i = 0; i < users.Count; i++)
                    {
                        if(users[i].UserPrivileges.Contains("CN"))
                        {
                            Contractors.Add(users[i]);
                        }
                        if (users[i].UserPrivileges.Contains("ES"))
                        {
                            Supervisors.Add(users[i]);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                //Dialogs.ShowError(e?.Message);
            }
        }

        public CertificateViewModel(ICertificatesService certificatesService, IBoardsService boardsService, IBuildingService buildingService, ILookupsService lookupsService)
        {
            Title = "Certificate Details - Basic Info";

            _certificatesService = certificatesService;
            _boardsService = boardsService;
            _buildingService = buildingService;
            _lookupsService = lookupsService;

            BasicInfoViewModel = new BasicInfoViewModel(certificatesService);
            AssociatedBoardsViewModel = new AssociatedBoardsViewModel(boardsService);
            EditPointViewModel = new EditPointViewModel(boardsService);

            Contractors = new List<BuildingUser>();
            Supervisors = new List<BuildingUser>();
            //CertDateAmendedOptions = new List<string>();
        }
    }
}