using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain;
using EDIS.Domain.Certificates;
using EDIS.Shared.Models;

namespace EDIS.Service.Interfaces
{
    public interface ICertificatesService
    {
        Task<ServiceResult> GetAllCertificates(CertificatesFilter filter, string buildingId, bool forceCloud = false);
        Task<ServiceResult> GetAllDownloadedCertificates(string buildingId);
        Task<ServiceResult> GetCertificateDetails(string buildingId, string certId, bool forceCloud = false);
        Task<ServiceResult> SaveCertificate(CertificateBasicInfo editedCertificate);
        Task<ServiceResult> UploadCertificate(string certId);
        Task<ServiceResult> DeleteCertificate(string certId, bool deleteOnCloud);
        Task<ServiceResult> GetEstateAndBuildingNames(string estateId, string buildingId);
    }
}