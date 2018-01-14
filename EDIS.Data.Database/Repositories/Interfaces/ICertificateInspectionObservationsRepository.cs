using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Certificates;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface ICertificateInspectionObservationsRepository : IRepository<CertificateInspectionObservations>
    {
        Task UpdateCertificateInspectionObservations(CertificateInspectionObservations obs);
    }
}