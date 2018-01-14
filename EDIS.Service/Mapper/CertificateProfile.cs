using System.Linq;
using EDIS.Domain.Certificates;
using EDIS.DTO.Responses;
using EDIS.Shared.Models;

namespace EDIS.Service.Mapper
{
    public class CertificateProfile
    {
        public CertificateProfile()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                /*cfg.CreateMap<Certificate, CertificateBasicInfo>()
                    .ForMember(x => x.CertId, opt => opt.MapFrom(k => k.CertId))
                    .ForMember(x => x.BuildingOrganizationResponsible, opt => opt.MapFrom(k => k.BuildingsContactTest.FirstOrDefault() != null ? k.BuildingsContactTest.FirstOrDefault().BuildingOrganizationResponsible : ""))
                    .ForMember(x => x.BuildingRespPersonAddress1, opt => opt.MapFrom(k => k.BuildingsContactTest.FirstOrDefault().BuildingRespPersonAddress1))
                    .ForMember(x => x.CertEdition, opt => opt.MapFrom(k => k.CertEdition))
                    .ForMember(x => x.CertDescription, opt => opt.MapFrom(k => k.CertDescription))
                    .ForMember(x => x.BuildingOccupierName, opt => opt.MapFrom(k => k.BuildingsContactTest.FirstOrDefault().BuildingOccupierName))
                    .ForMember(x => x.BuildingOccupierAddress1, opt => opt.MapFrom(k => k.BuildingsContactTest.FirstOrDefault().BuildingOccupierAddress1))
                    .ForMember(x => x.BuildingAgeOfInstallation, opt => opt.MapFrom(k => k.BuildingsTest.FirstOrDefault().BuildingAgeOfInstallation))
                    .ForMember(x => x.InstallationRecordsLocation, opt => opt.MapFrom(k => k.BuildingsTest.FirstOrDefault().InstallationRecordsLocation))
                    .ForMember(x => x.RecordsHeldBy, opt => opt.MapFrom(k => k.BuildingsTest.FirstOrDefault().RecordsHeldBy))
                    .ForMember(x => x.CertInspectionBuildingEvidenceOfAlteration, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionBuildingEvidenceOfAlteration))
                    .ForMember(x => x.CertInspectionBuildingAgeOfAlteration, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionBuildingAgeOfAlteration))
                    .ForMember(x => x.CertInspectionPreviousInspectionDate, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionDate))
                    .ForMember(x => x.CertInspectionPreviousInspectionComments, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionComments))
                    .ForMember(x => x.CertInspectionPreviousCertNumber, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousCertNumber))
                    .ForMember(x => x.CertInspectionExtentCovered, opt => opt.MapFrom(k => k.CertificateInspections.FirstOrDefault().CertInspectionExtentCovered))
                    .ForMember(x => x.CertAgreedLimitations, opt => opt.MapFrom(k => k.CertAgreedLimitations))
                    .ForMember(x => x.CertAgreedWith, opt => opt.MapFrom(k => k.CertAgreedWith))
                    .ForMember(x => x.CertOperationalLimitations, opt => opt.MapFrom(k => k.CertOperationalLimitations));

                cfg.CreateMap<CertificateBasicInfo, Certificate>()
                    .ForMember(k => k.BuildingsContactTest.FirstOrDefault().BuildingOrganizationResponsible, opt => opt.MapFrom(x => x.BuildingOrganizationResponsible))
                    .ForMember(k => k.BuildingsContactTest.FirstOrDefault().BuildingRespPersonAddress1, opt => opt.MapFrom(x => x.BuildingRespPersonAddress1))
                    .ForMember(k => k.CertEdition, opt => opt.MapFrom(x => x.CertEdition))
                    .ForMember(k => k.CertDescription, opt => opt.MapFrom(x => x.CertDescription))
                    .ForMember(k => k.BuildingsContactTest.FirstOrDefault().BuildingOccupierName, opt => opt.MapFrom(x => x.BuildingOccupierName))
                    .ForMember(k => k.BuildingsContactTest.FirstOrDefault().BuildingOccupierAddress1, opt => opt.MapFrom(x => x.BuildingOccupierAddress1))
                    .ForMember(k => k.BuildingsTest.FirstOrDefault().BuildingAgeOfInstallation, opt => opt.MapFrom(x => x.BuildingAgeOfInstallation))
                    .ForMember(k => k.BuildingsTest.FirstOrDefault().InstallationRecordsLocation, opt => opt.MapFrom(x => x.InstallationRecordsLocation))
                    .ForMember(k => k.BuildingsTest.FirstOrDefault().RecordsHeldBy, opt => opt.MapFrom(x => x.RecordsHeldBy))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionBuildingEvidenceOfAlteration, opt => opt.MapFrom(x => x.CertInspectionBuildingEvidenceOfAlteration))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionBuildingAgeOfAlteration, opt => opt.MapFrom(x => x.CertInspectionBuildingAgeOfAlteration))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionDate, opt => opt.MapFrom(x => x.CertInspectionPreviousInspectionDate))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionComments, opt => opt.MapFrom(x => x.CertInspectionPreviousInspectionComments))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionPreviousCertNumber, opt => opt.MapFrom(x => x.CertInspectionPreviousCertNumber))
                    .ForMember(k => k.CertificateInspections.FirstOrDefault().CertInspectionExtentCovered, opt => opt.MapFrom(x => x.CertInspectionExtentCovered))
                    .ForMember(x => x.CertAgreedLimitations, opt => opt.MapFrom(k => k.CertAgreedLimitations))
                    .ForMember(x => x.CertAgreedWith, opt => opt.MapFrom(k => k.CertAgreedWith))
                    .ForMember(x => x.CertOperationalLimitations, opt => opt.MapFrom(k => k.CertOperationalLimitations));*/
            });
        }
    }
}