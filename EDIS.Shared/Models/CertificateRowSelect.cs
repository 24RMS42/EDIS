using EDIS.Domain.Certificates;
using EDIS.Shared.ViewModels.Base;

namespace EDIS.Shared.Models
{
    public class CertificateRowSelect : BaseViewModel
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        private CertificateRow _certificate;

        public CertificateRow Certificate
        {
            get { return _certificate; }
            set { Set(() => Certificate, ref _certificate, value); }
        }
    }
}