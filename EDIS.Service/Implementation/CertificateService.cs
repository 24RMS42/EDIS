using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain;
using EDIS.Domain.Buildings;
using EDIS.Domain.Certificates;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;
using EDIS.Service.Mapper;
using EDIS.Shared.Models;

namespace EDIS.Service.Implementation
{
    public class CertificateService : BaseService, ICertificatesService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<ServiceResult> GetAllCertificates(CertificatesFilter filter, string buildingId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var certificatesDb = await DbManager.CertificateRowRepository.GetAll(x => x.BuildingId == buildingId);
                if (certificatesDb.Any())
                    return new SuccessServiceResult { ResultObject = new Tuple<IEnumerable<CertificateRow>, int>(certificatesDb, certificatesDb.Count) };
            }

            var url = GlobalSettings.BaseURL + "/certificates";

            var response = await _requestProvider.PostAsync<CertificatesRequest, CertificatesResponse>(url, new CertificatesRequest
            {
                Token = Settings.AccessToken,
                BuildingId = buildingId,
                Limit = new List<int>() { 0, 20 },
                Filter = filter
            });

            if (response != null && response.Certificates.Any())
            {
                foreach (var building in response.Certificates)
                {
                    building.BuildingId = buildingId;
                }

                if (forceCloud)
                    await DbManager.CertificateRowRepository.DeleteAll();
                await DbManager.CertificateRowRepository.AddMany(response.Certificates);

                return new SuccessServiceResult { ResultObject = new Tuple<IEnumerable<CertificateRow>, int, int>(response.Certificates, response.CertificatesFound, response.CertificatesReturned) };
            }

            return new FalseServiceResult("Error occured");
        }

        public async Task<ServiceResult> GetCertificateDetails(string buildingId, string certId, bool forceCloud = false)
        {

            if (!forceCloud)
            {
                var certificateDb = await DbManager.CertificateRepository.GetCertificate(certId);
                if (certificateDb != null)
                {
                    //var basicInfo = AutoMapper.Mapper.Map<CertificateBasicInfo>(certificateDb);
                    var basicInfo = MapCertificateToBasicInfo(certificateDb, new CertificateBasicInfo());
                    var editCertificate = new EditCertificate
                    {
                        CertificateBasicInfo = basicInfo,
                        CertificateAssociatedBoards = { BoardTests = certificateDb.BoardsTest }
                    };

                    return new SuccessServiceResult { ResultObject = editCertificate };
                }

            }

            var url = GlobalSettings.BaseURL + "/certificates/detail";

            var response = await _requestProvider.PostAsync<CertificateRequest, CertificateResponse>(url,
                new CertificateRequest
                {
                    Token = Settings.AccessToken,
                    BuildingId = buildingId,
                    CertId = certId
                });

            if (response != null)
            {
                if (forceCloud)
                {
                    await DbManager.CertificateRepository
                        .DeleteCertificate(certId); //Todo: When there is unsaved data
                }

                /*                   
                    cert.BoardsTest = response.BoardsTest;
                    cert.BuildingsContactTest = response.BuildingsContactTest;
                    cert.BuildingsTest = response.BuildingsTest;
                    cert.CertificateInspections = response.CertificateInspections;
                    cert.CircuitsTest = response.CircuitsTest;
                    await DbManager.CertificateRepository.Add(cert);
                */

                var cert = response.Certificates.FirstOrDefault();
                if (cert != null)
                {
                    await DbManager.CertificateRepository.Add(cert);

                    await DbManager.BoardTestRepository.AddMany(response.BoardsTest);
                    cert.BoardsTest = response.BoardsTest;

                    await DbManager.BuildingContactTestRepository.AddMany(response.BuildingsContactTest);
                    cert.BuildingsContactTest = response.BuildingsContactTest;

                    await DbManager.BuildingTestRepository.AddMany(response.BuildingsTest);
                    cert.BuildingsTest = response.BuildingsTest;

                    await DbManager.CertificateInspectionRepository.AddMany(response.CertificateInspections);
                    cert.CertificateInspections = response.CertificateInspections;

                    await DbManager.CircuitTestRepository.AddMany(response.CircuitsTest);
                    cert.CircuitsTest = response.CircuitsTest;

                    await DbManager.CertificateInspectionObservationsRepository.AddMany(response.CertificateInspectionsObservations);
                    cert.CertificatesInspectionObservations = response.CertificateInspectionsObservations;

                    await DbManager.CircuitPointsRcdTestRepository.AddMany(response.CircuitsPointsRcdTest);
                    cert.CircuitsPointsRcdTest = response.CircuitsPointsRcdTest;
                }

                //var basicInfo = AutoMapper.Mapper.Map<CertificateBasicInfo>(cert);
                var basicInfo = MapCertificateToBasicInfo(cert, new CertificateBasicInfo());
                var editCertificate = new EditCertificate
                {
                    CertificateBasicInfo = basicInfo,
                    CertificateAssociatedBoards = { BoardTests = cert?.BoardsTest }
                };
                return new SuccessServiceResult { ResultObject = editCertificate };
            }

            return new FalseServiceResult("Response is null");
        }

        public async Task<ServiceResult> GetAllDownloadedCertificates(string buildingId)
        {
            var certificates = await DbManager.CertificateRepository.GetAll(x => x.BuildingId == buildingId);
            return new SuccessServiceResult { ResultObject = certificates };
        }

        public async Task<ServiceResult> SaveCertificate(CertificateBasicInfo editedCertificate)
        {
            var cert = await DbManager.CertificateRepository.GetCertificate(editedCertificate.CertId);
            //var certificate = AutoMapper.Mapper.Map(editedCertificate, cert);
            var certificate = MapBasicInfoToCertificate(cert, editedCertificate);
            await DbManager.CertificateRepository.UpdateCertificate(certificate);

            cert = await DbManager.CertificateRepository.GetCertificate(editedCertificate.CertId);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> UploadCertificate(string certId)
        {
            var certificate = await DbManager.CertificateRepository.GetCertificate(certId);

            if (certificate == null)
                return new FalseServiceResult("Error occured");

            var url = GlobalSettings.BaseURL + "/certificates/upload";

            var response = await _requestProvider.PostAsync<UploadCertificateRequest, UploadCertificateResponse>(url,
                new UploadCertificateRequest
                {
                    Token = Settings.AccessToken,
                    BuildingId = certificate.BuildingId,
                    CertId = certificate.CertId,
                    Certificates = new List<Certificate> { certificate },
                    CertificateInspections = certificate.CertificateInspections,
                    BuildingsTest = certificate.BuildingsTest,
                    BoardsTest = certificate.BoardsTest,
                    BuildingsContactTest = certificate.BuildingsContactTest,
                    CircuitsTest = certificate.CircuitsTest,
                    CertificatesInspectionObservations = certificate.CertificatesInspectionObservations,
                    CertificatesInspectionSchedule = new List<string>(),
                    CircuitsPointsRcdTest = certificate.CircuitsPointsRcdTest,
                    CertExtraSeos = new List<string>(),
                    SupplyEarthingOriginTests = new List<string>()
                });

            if (response != null)
            {
                return new SuccessServiceResult { ResultObject = response.Message };
            }

            return new FalseServiceResult("Error occured");
        }

        public async Task<ServiceResult> DeleteCertificate(string certId, bool deleteOnCloud)
        {
            try
            {
                var certificate = await DbManager.CertificateRepository.FindByQuery(x => x.CertId == certId);

                var url = GlobalSettings.BaseURL + "/certificates/delete";

                DeleteCertificateResponse response;
                
                if (deleteOnCloud)
                {
                    response = await _requestProvider.PostAsync<DeleteCertificateRequest, DeleteCertificateResponse>(url,
                            new DeleteCertificateRequest
                            {
                                Token = Settings.AccessToken,
                                CertId = certId,
                                BuildingId = certificate.BuildingId
                            });
                }

                var cprs = await DbManager.CircuitPointsRcdTestRepository.GetAll(x => x.CertId == certId);

                await DbManager.CertificateRepository.DeleteCertificate(certId);

                cprs = await DbManager.CircuitPointsRcdTestRepository.GetAll(x => x.CertId == certId);

                return new SuccessServiceResult();
            }
            catch (Exception e)
            {
                return new FalseServiceResult(e.Message);
            }
        }

        public async Task<ServiceResult> GetEstateAndBuildingNames(string estateId, string buildingId)
        {
            var estate = await DbManager.EstateRepository.FindByQuery(x => x.EstateId == estateId);
            var building = await DbManager.BuildingRowRepository.FindByQuery(x => x.BuildingId == buildingId);

            if(estate == null || building == null)
                return new FalseServiceResult("");

            return new SuccessServiceResult{ResultObject = new Tuple<string, string>(estate.EstateName, building.BuildingName)};
        }

        public CertificateService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
            //new CertificateProfile();
        }

        private CertificateBasicInfo MapCertificateToBasicInfo(Certificate certificate, CertificateBasicInfo basicInfo)
        {
            basicInfo.CertId = certificate.CertId;
            basicInfo.BuildingOrganizationResponsible = certificate.BuildingsContactTest.FirstOrDefault()?.BuildingOrganizationResponsible;
            basicInfo.BuildingRespPersonAddress1 = certificate.BuildingsContactTest.FirstOrDefault()?.BuildingRespPersonAddress1;
            if (certificate.CertDateCreated != null) basicInfo.CertDateCreated = certificate.CertDateCreated.Value;
            basicInfo.CertEdition = certificate.CertEdition;
            basicInfo.CertDescription = certificate.CertDescription;
            basicInfo.BuildingOccupierName = certificate.BuildingsContactTest.FirstOrDefault()?.BuildingOccupierName;
            basicInfo.BuildingOccupierAddress1 = certificate.BuildingsContactTest.FirstOrDefault()?.BuildingOccupierAddress1;
            basicInfo.BuildingAgeOfInstallation = certificate.BuildingsTest.FirstOrDefault()?.BuildingAgeOfInstallation;
            basicInfo.InstallationRecordsLocation = certificate.BuildingsTest.FirstOrDefault()?.InstallationRecordsLocation;
            basicInfo.RecordsHeldBy = certificate.BuildingsTest.FirstOrDefault()?.RecordsHeldBy;
            basicInfo.CertInspectionBuildingEvidenceOfAlteration = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionBuildingEvidenceOfAlteration;
            basicInfo.CertInspectionBuildingAgeOfAlteration = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionBuildingAgeOfAlteration;
            basicInfo.CertInspectionPreviousInspectionDate = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionPreviousInspectionDate;
            basicInfo.CertInspectionPreviousInspectionComments = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionPreviousInspectionComments;
            basicInfo.CertInspectionPreviousCertNumber = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionPreviousCertNumber;
            basicInfo.CertInspectionExtentCovered = certificate.CertificateInspections.FirstOrDefault()?.CertInspectionExtentCovered;
            basicInfo.CertAgreedLimitations = certificate.CertAgreedLimitations;
            basicInfo.CertAgreedWith = certificate.CertAgreedWith;
            basicInfo.CertOperationalLimitations = certificate.CertOperationalLimitations;
            basicInfo.ConUserId = certificate.ConUserId;
            basicInfo.EsUserId = certificate.EsUserId;
            basicInfo.CertDateAmended = certificate.CertDateAmended;

            if (certificate.BuildingsTest.Any() && certificate.BuildingsTest.FirstOrDefault() != null)
            {
                var description = certificate.BuildingsTest.FirstOrDefault()?.BuildingPremises;
                var other = certificate.BuildingsTest.FirstOrDefault()?.BuildingPremisesOther;
                var premises = "";
                switch (description)
                {
                    case "O":
                        premises = "Other " + other;
                        break;
                    case "C":
                        premises = "Commercial";
                        break;
                    case "D":
                        premises = "Domestic";
                        break;
                    case "I":
                        premises = "Industrial";
                        break;
                }
                basicInfo.DescriptionOfPremises = premises;
            }

            return basicInfo;
        }

        private Certificate MapBasicInfoToCertificate(Certificate certificate, CertificateBasicInfo basicInfo)
        {
            certificate.BuildingsContactTest.FirstOrDefault().BuildingOrganizationResponsible = basicInfo.BuildingOrganizationResponsible;
            certificate.BuildingsContactTest.FirstOrDefault().BuildingRespPersonAddress1 = basicInfo.BuildingRespPersonAddress1;
            certificate.CertDateCreated = basicInfo.CertDateCreated;
            certificate.CertEdition = basicInfo.CertEdition;
            certificate.CertDescription = basicInfo.CertDescription;
            certificate.BuildingsContactTest.FirstOrDefault().BuildingOccupierName = basicInfo.BuildingOccupierName;
            certificate.BuildingsContactTest.FirstOrDefault().BuildingOccupierAddress1 = basicInfo.BuildingOccupierAddress1;
            if (basicInfo.BuildingAgeOfInstallation != null)
                certificate.BuildingsTest.FirstOrDefault().BuildingAgeOfInstallation = basicInfo.BuildingAgeOfInstallation.Value;
            certificate.BuildingsTest.FirstOrDefault().InstallationRecordsLocation = basicInfo.InstallationRecordsLocation;
            certificate.BuildingsTest.FirstOrDefault().RecordsHeldBy = basicInfo.RecordsHeldBy;
            certificate.CertificateInspections.FirstOrDefault().CertInspectionBuildingEvidenceOfAlteration = basicInfo.CertInspectionBuildingEvidenceOfAlteration;
            if (basicInfo.CertInspectionBuildingAgeOfAlteration != null)
                certificate.CertificateInspections.FirstOrDefault().CertInspectionBuildingAgeOfAlteration = basicInfo.CertInspectionBuildingAgeOfAlteration.Value;
            certificate.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionDate = basicInfo.CertInspectionPreviousInspectionDate;
            certificate.CertificateInspections.FirstOrDefault().CertInspectionPreviousInspectionComments = basicInfo.CertInspectionPreviousInspectionComments;
            certificate.CertificateInspections.FirstOrDefault().CertInspectionPreviousCertNumber = basicInfo.CertInspectionPreviousCertNumber;
            certificate.CertificateInspections.FirstOrDefault().CertInspectionExtentCovered = basicInfo.CertInspectionExtentCovered;
            certificate.CertAgreedLimitations = basicInfo.CertAgreedLimitations;
            certificate.CertAgreedWith = basicInfo.CertAgreedWith;
            certificate.CertOperationalLimitations = basicInfo.CertOperationalLimitations;
            certificate.ConUserId = basicInfo.ConUserId;
            certificate.EsUserId = basicInfo.EsUserId;
            certificate.CertDateAmended = basicInfo.CertDateAmended;

            return certificate;
        }
    }
}