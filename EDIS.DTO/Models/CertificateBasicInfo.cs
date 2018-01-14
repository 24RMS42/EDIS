using System;

namespace EDIS.Shared.Models
{
    public class CertificateBasicInfo
    {
        public string CertId { get; set; }
        public string BuildingOrganizationResponsible { get; set; }
        public string BuildingRespPersonAddress1 { get; set; }
        public DateTime CertDateCreated { get; set; }
        public string CertEdition { get; set; }
        public string CertDescription { get; set; }
        public string BuildingOccupierName { get; set; }
        public string BuildingOccupierAddress1 { get; set; }
        public int? BuildingAgeOfInstallation { get; set; }
        public string InstallationRecordsLocation { get; set; }
        public string RecordsHeldBy { get; set; }
        public string CertInspectionBuildingEvidenceOfAlteration { get; set; }
        public int? CertInspectionBuildingAgeOfAlteration { get; set; }
        public DateTime? CertInspectionPreviousInspectionDate { get; set; }
        public string CertInspectionPreviousInspectionComments { get; set; }
        public string CertInspectionPreviousCertNumber { get; set; }
        public string CertInspectionExtentCovered { get; set; }
        public string CertAgreedLimitations { get; set; }
        public string CertAgreedWith { get; set; }
        public string CertOperationalLimitations { get; set; }
        public string DescriptionOfPremises { get; set; }
        public string ConUserId { get; set; }
        public string EsUserId { get; set; }
        public string CertDateAmended { get; set; }
    }
}