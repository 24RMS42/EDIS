using EDIS.Domain.Buildings;
using EDIS.Domain.Lookups;
using EDIS.Service.Interfaces;
using EDIS.Shared.Models;
using EDIS.Shared.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDIS.Shared.ViewModels.Certificates
{
    public class BasicInfoViewModel : BaseViewModel
    {
        private ICertificatesService _certificatesService;
        //private IBuildingService _buildingService;

        private CertificateBasicInfo _certificate;
        public CertificateBasicInfo Certificate
        {
            get => _certificate;
            set { Set(() => Certificate, ref _certificate, value); }
        }

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

        private BuildingUser _selectedContractor;
        public BuildingUser SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                Set(() => SelectedContractor, ref _selectedContractor, value);
                Certificate.ConUserId = value?.UserId;
            }
        }

        private BuildingUser _selectedSupervisor;
        public BuildingUser SelectedSupervisor
        {
            get { return _selectedSupervisor; }
            set
            {
                Set(() => SelectedSupervisor, ref _selectedSupervisor, value);
                Certificate.EsUserId = value?.UserId;
            }
        }

        private List<BsAmendmentDates> _bsAmendmentDates;
        public List<BsAmendmentDates> BsAmendmentDates
        {
            get { return _bsAmendmentDates; }
            set { Set(() => BsAmendmentDates, ref _bsAmendmentDates, value); }
        }

        private BsAmendmentDates _selectedBsAmendmentDate;
        public BsAmendmentDates SelectedBsAmendmentDate
        {
            get { return _selectedBsAmendmentDate; }
            set
            {
                Set(() => SelectedBsAmendmentDate, ref _selectedBsAmendmentDate, value);
                Certificate.CertDateAmended = value?.CertDateAmended;
            }
        }

        private RelayCommand _saveCertificateCommand;

        public RelayCommand SaveCertificateCommand
        {
            get
            {
                return _saveCertificateCommand ?? (_saveCertificateCommand = new RelayCommand(async () =>
                {
                    await _certificatesService.SaveCertificate(Certificate);
                    Dialogs.Toast("Successful");
                }));
            }
        }

        public BasicInfoViewModel(ICertificatesService certificatesService)
        {
            Title = "Basic info";

            _certificatesService = certificatesService;
            //_buildingService = buildingService;
            Certificate = new CertificateBasicInfo();

            
            Contractors = new List<BuildingUser>();
            Supervisors = new List<BuildingUser>();
            BsAmendmentDates = new List<BsAmendmentDates>();

        }
    }
}