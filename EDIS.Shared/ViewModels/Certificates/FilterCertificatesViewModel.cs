using System;
using EDIS.Domain;
using EDIS.Shared.Helpers;
using EDIS.Shared.ViewModels.Base;
using EDIS.Shared.ViewModels.Boards;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class FilterCertificatesViewModel : BaseViewModel
    {
        private CertificatesFilter _filters;

        public CertificatesFilter Filters
        {
            get { return _filters; }
            set { Set(() => Filters, ref _filters, value); }
        }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(async () =>
                {
                    await NavigationService.HideModalPage();
                }));
            }
        }

        private DateTime _fromDateOfCreation = DateTime.Today.AddDays(-7); 

        public DateTime FromDateOfCreation
        {
            get { return _fromDateOfCreation; }
            set { Set(() => FromDateOfCreation, ref _fromDateOfCreation, value); }
        }

        private DateTime _toDateOfCreation = DateTime.Today;

        public DateTime ToDateOfCreation
        {
            get { return _toDateOfCreation; }
            set { Set(() => ToDateOfCreation, ref _toDateOfCreation, value); }
        }

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(async () =>
                {
                    if (FromDateOfCreation > ToDateOfCreation)
                    {
                        Dialogs.Alert("Please check selected dates!");
                        return;
                    }

                    Filters.CertStatusFrom = FromDateOfCreation;
                    ToDateOfCreation = ToDateOfCreation.AddHours(23);
                    ToDateOfCreation = ToDateOfCreation.AddMinutes(59);
                    Filters.CertStatusTo = ToDateOfCreation;

                    await NavigationService.HideModalPage();
                    await NavigationService.NavigateToAsync<AllCertificatesViewModel>(Filters);
                    
                }));
            }
        }

        public FilterCertificatesViewModel()
        {
            Title = "Filter";

            Filters = new CertificatesFilter()
            {
                CertType = "scr",
                CertStatus = "draft",
            };
        }
    }
}