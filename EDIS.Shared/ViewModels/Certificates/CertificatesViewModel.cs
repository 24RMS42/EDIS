using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EDIS.Core;
using EDIS.Core.Exceptions;
using EDIS.Domain;
using EDIS.Domain.Certificates;
using EDIS.Service.Implementation;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class CertificatesViewModel : BaseViewModel
    {
        private readonly ICertificatesService _certificatesService;
        private readonly IBuildingService _buildingService;

        public ObservableCollectionFast<Certificate> Certificates { get; set; }

        public bool NoCertificates => !Certificates.Any();

        private bool _deleteFromCloud;

        public bool DeleteFromCloud
        {
            get { return _deleteFromCloud; }
            set { Set(() => DeleteFromCloud, ref _deleteFromCloud, value); }
        }

        private string _estateName;

        public string EstateName
        {
            get { return string.IsNullOrEmpty(_estateName) ? "n/a" : _estateName; }
            set { Set(() => EstateName, ref _estateName, value); }
        }

        private string _buildingName;

        public string BuildingName
        {
            get { return string.IsNullOrEmpty(_buildingName) ? "n/a" : _buildingName; }
            set { Set(() => BuildingName, ref _buildingName, value); }
        }

        private bool _showDeleteDialog;

        public bool ShowDeleteDialog
        {
            get { return _showDeleteDialog; }
            set { Set(() => ShowDeleteDialog, ref _showDeleteDialog, value); }
        }

        private Certificate _certificateSelected;

        public Certificate CertificateSelected
        {
            get { return _certificateSelected; }
            set { Set(() => CertificateSelected, ref _certificateSelected, value); }
        }

        private RelayCommand _closeDeleteDialog;

        public RelayCommand CloseDeleteDialog
        {
            get
            {
                return _closeDeleteDialog ?? (_closeDeleteDialog = new RelayCommand(() =>
                {
                    ShowDeleteDialog = false;
                }));
            }
        }

        private RelayCommand _deleteCertificateCommand;

        public RelayCommand DeleteCertificateCommand
        {
            get
            {
                return _deleteCertificateCommand ?? (_deleteCertificateCommand = new RelayCommand(async () =>
                {
                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        try
                        {
                            RaisePropertyChanged(() => CertificateSelected);
                            var result = await _certificatesService.DeleteCertificate(CertificateSelected.CertId, DeleteFromCloud);
                            if (!result.Success)
                            {
                                Dialogs.Toast(result.Message);
                                return;
                            }

                            Certificates.Remove(Certificates.FirstOrDefault(x => x.CertId == CertificateSelected.CertId));

                            ShowDeleteDialog = false;

                            await Dialogs.ConfirmAsync("Successful", "Delete", "OK");
                        }
                        catch (ServiceAuthenticationException e)
                        {
                            var result = await TryToLogin();
                            if (!result)
                                await NavigationService.NavigateToAsync<LoginViewModel>();
                            else
                                DeleteCertificateCommand.Execute(null);
                        }
                        catch (Exception e)
                        {
                            dlg.Hide();
                            await ShowErrorAlert(e.Message);
                        }
                    }
                }));
            }
        }

        private RelayCommand<Certificate> _certificatedSelectedCommand;

        public RelayCommand<Certificate> CertificatedSelectedCommand
        {
            get
            {
                return _certificatedSelectedCommand ?? (_certificatedSelectedCommand = new RelayCommand<Certificate>(async (certificate) =>
                {
                    if (certificate == null)
                        return;

                    CertificateSelected = certificate;
                    CertificateId = certificate.CertId;

                    using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                    {
                        
                        try
                        {
                            dlg.Hide();
                            var response = await Dialogs.ActionSheetAsync(null, "Cancel", null, null,
                                "Edit", "Upload", "Delete");
                            switch (response)
                            {
                                case "Edit":
                                    dlg.Show();
                                    await Task.Delay(200);
                                    await NavigationService.NavigateToAsync<CertificateViewModel>(certificate.CertId);
                                    break;
                                case "Upload":
                                {
                                    dlg.Show();
                                    var result = await _certificatesService.UploadCertificate(certificate.CertId);
                                    if (!result.Success)
                                    {
                                        Dialogs.Toast(result.Message);
                                        return;
                                    }
                                    dlg.Hide();
                                    await Dialogs.ConfirmAsync("Finished", "Upload", "OK");
                                    break;
                                }
                                case "Download":
                                    break;
                                case "Copy":
                                    break;
                                case "Delete":
                                {
                                    ShowDeleteDialog = true;
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                        catch (ServiceAuthenticationException e)
                        {
                            var result = await TryToLogin();
                            if (!result)
                                await NavigationService.NavigateToAsync<LoginViewModel>();
                            else
                                CertificatedSelectedCommand.Execute(certificate);
                        }
                        catch (Exception e)
                        {
                            dlg.Hide();
                            await ShowErrorAlert(e.Message);
                        }
                    }
                }));
            }
        }

        private RelayCommand _refresh;

        public RelayCommand Refresh
        {
            get
            {
                return _refresh ?? (_refresh = new RelayCommand(async () =>
                {
                    await NavigationService.ModalNavigateToAsync<FilterCertificatesViewModel>(null);
                }));
            }
        }

        private async Task GetCertificates()
        {
            try
            {
                using (this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    Certificates.Clear();

                    var buildingUsers = await _buildingService.GetBuildingUsers(BuildingId, false);
                    var cnUsers = buildingUsers.Where(x => x.UserPrivileges.Contains("CN")).ToList();

                    var certsResult = await _certificatesService.GetAllDownloadedCertificates(BuildingId);
                    if (!certsResult.Success)
                    {
                        Dialogs.Toast(certsResult.Message);
                        return;
                    }

                    var certificates = certsResult.ResultObject as List<Certificate>;

                    if (certificates != null && certificates.Any())
                    {
                        certificates = new List<Certificate>(certificates.OrderBy(x => x.CertNumber));

                        foreach (var cert in certificates)
                        {
                            cert.Contractor = cnUsers?.FirstOrDefault(x => x.UserId == cert.ConUserId)?.UserFullname;
                        }

                        Certificates.AddRange(certificates);
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await GetCertificates();
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public override async Task OnAppearing()
        {
            MessagingCenter.Send(this, MessengerCenterMessages.RefreshApiOnMenu);


            if (string.IsNullOrEmpty(EstateId) || string.IsNullOrEmpty(BuildingId))
            {
                Dialogs.Toast("Please select estate/building!");
                return;
            }

            var names = await _certificatesService.GetEstateAndBuildingNames(EstateId, BuildingId);
            if (!names.Success)
                return;

            var response = names.ResultObject as Tuple<string, string>;
            if(response == null)
                return;

            EstateName = response?.Item1;
            BuildingName = response?.Item2;

            CommonSettings.EstateName = EstateName;
            CommonSettings.BuildingName = BuildingName;
            CommonSettings.BuildingPrivileges = Settings.Privileges;

            await GetCertificates();

            RaisePropertyChanged(() => NoCertificates);

            
        }

        public CertificatesViewModel(ICertificatesService certificatesService, IBuildingService buildingService)
        {
            Title = "Certificates";

            _certificatesService = certificatesService;
            _buildingService = buildingService;

            Certificates = new ObservableCollectionFast<Certificate>();
        }
    }
}