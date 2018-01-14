namespace EDIS.Shared.Models
{
    public class EditCertificate
    {
        public CertificateBasicInfo CertificateBasicInfo { get; set; }
        public CertificateAssociatedBoards CertificateAssociatedBoards { get; set; }

        public EditCertificate()
        {
            CertificateBasicInfo = new CertificateBasicInfo();
            CertificateAssociatedBoards = new CertificateAssociatedBoards();
        }
    }
}