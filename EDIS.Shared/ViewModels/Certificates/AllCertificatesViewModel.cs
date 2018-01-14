using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain;
using EDIS.Domain.Certificates;
using EDIS.Service.Interfaces;
using EDIS.Shared.Extensions;
using EDIS.Shared.Helpers;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class AllCertificatesViewModel : BaseViewModel
    {
        private readonly ICertificatesService _certificatesService;
        private readonly IBuildingService _buildingService;

        private CertificatesFilter Filter;

        public List<Certificate> DownloadedCertificates { get; set; }

        public ObservableCollectionFast<CertificateRowSelect> Certificates { get; set; }

        private RelayCommand<CertificateRowSelect> _certificateSelectedCommand;

        public RelayCommand<CertificateRowSelect> CertificateSelectedCommand
        {
            get
            {
                return _certificateSelectedCommand ?? (_certificateSelectedCommand = new RelayCommand<CertificateRowSelect>(cert =>
                {
                    if (cert == null)
                        return;

                    cert.IsSelected = !cert.IsSelected;
                }));
            }
        }

        private RelayCommand _importCertificatesCommand;

        public RelayCommand ImportCertificatesCommand
        {
            get
            {
                return _importCertificatesCommand ?? (_importCertificatesCommand = new RelayCommand(async () =>
                {
                    try
                    {
                        var errorCheck = false;

                        using (var d = Dialogs.Loading("Progress"))
                        {
                            foreach (var certificate in Certificates)
                            {
                                if (DownloadedCertificates.Any(x => x.CertId == certificate.Certificate.CertId))
                                {
                                    if (certificate.IsSelected)
                                    {
                                        d.Hide();
                                        var response = await Dialogs.ConfirmAsync( "There is already data for certificate: " +
                                                                                   certificate.Certificate.CertNumber +
                                                                                   " in database, do you want to overwrite it?", "Overwrite", "Yes", "No");
                                        d.Show();
                                        if(!response)
                                            continue;
                                    }
                                }

                                if (certificate.IsSelected)
                                {
                                    var result = await _certificatesService.GetCertificateDetails(BuildingId, certificate.Certificate.CertId, true);
                                    if (!result.Success)
                                        errorCheck = true;
                                }
                            }
                            if (errorCheck)
                            {
                                Dialogs.ShowError("Error occured");
                            }
                            else
                            {
                                await NavigationService.NavigateBackAsync();
                            }
                        }
                    }
                    catch (ServiceAuthenticationException e)
                    {
                        var result = await TryToLogin();
                        if (!result)
                            await NavigationService.NavigateToAsync<LoginViewModel>();
                        else
                            ImportCertificatesCommand.Execute(null);
                    }
                    catch (Exception e)
                    {
                        await ShowErrorAlert(e.Message);
                    }
                }));
            }
        }

        public override async Task OnAppearing()
        {
            if(Filter != null)
                await GetCertificates(Filter, true);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var filters = navigationData as CertificatesFilter;
            Filter = filters;
            await GetCertificates(Filter, true);
        }

        private async Task GetCertificates(CertificatesFilter filters, bool forceCloud = false)
        {
            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    Certificates.Clear();

                    var buildingUsers = await _buildingService.GetBuildingUsers(BuildingId, false);
                    var cnUsers = buildingUsers.Where(x => x.UserPrivileges.Contains("CN")).ToList();

                    var downloadedCertsResult = await _certificatesService.GetAllDownloadedCertificates(BuildingId);
                    if (!downloadedCertsResult.Success)
                    {
                        Dialogs.Toast(downloadedCertsResult.Message);
                        return;
                    }

                    var downloadedCertificates = downloadedCertsResult.ResultObject as List<Certificate>;

                    if (downloadedCertificates != null)
                    {
                        DownloadedCertificates = downloadedCertificates;

                        var certsResult = await _certificatesService.GetAllCertificates(filters, BuildingId, forceCloud);
                        if (!certsResult.Success)
                        {
                            Dialogs.Toast(certsResult.Message);
                            return;
                        }

                        var certsAndNumber = certsResult.ResultObject as Tuple<IEnumerable<CertificateRow>, int, int>;
                        if (certsAndNumber == null)
                            return;

                        Dialogs.Toast("Results; " + certsAndNumber.Item3 + " records returned out of " + certsAndNumber.Item2 + " found.");

                        var certificates = certsAndNumber.Item1;

                        if (certificates != null && certificates.Any())
                        {
                            foreach (var certificate in certificates)
                            {
                                var selectableCertificate = new CertificateRowSelect() { Certificate = certificate, IsSelected = false };
                                //if (downloadedCertificates.Any(x => x.CertId == certificate.CertId))
                                //    selectableCertificate.IsSelected = true;
                                Certificates.Add(selectableCertificate);
                            }
                        }

                        foreach (var cert in Certificates)
                        {
                            cert.Certificate.Contractor = cnUsers?.FirstOrDefault(x => x.UserId == cert.Certificate.ConUserId)?.UserFullname;
                        }
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await GetCertificates(filters, forceCloud);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e.Message);
            }
        }

        public AllCertificatesViewModel(ICertificatesService certificatesService, IBuildingService buildingService)
        {
            Title = "Certificates on cloud";

            _certificatesService = certificatesService;
            _buildingService = buildingService;
            Certificates = new ObservableCollectionFast<CertificateRowSelect>();
            DownloadedCertificates = new List<Certificate>();
        }
    }
}