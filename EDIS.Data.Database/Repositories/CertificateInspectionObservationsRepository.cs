using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Certificates;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class CertificateInspectionObservationsRepository : Repository<CertificateInspectionObservations>, ICertificateInspectionObservationsRepository
    {
        public async Task UpdateCertificateInspectionObservations(CertificateInspectionObservations obs)
        {
            await Connection.InsertOrReplaceWithChildrenAsync(obs, true);
        }
    }
}