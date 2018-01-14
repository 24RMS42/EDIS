using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Certificates;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<Certificate> GetCertificate(string certId);
        Task UpdateCertificate(Certificate certificate);
        Task DeleteCertificate(string certId);
    }
}