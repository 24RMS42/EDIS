using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Certificates;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        public async Task<Certificate> GetCertificate(string certId)
        {
            var cert = await Connection.Table<Certificate>().Where(x => x.CertId == certId).FirstOrDefaultAsync();
            if(cert != null)
                return await Connection.GetWithChildrenAsync<Certificate>(certId);
            return null;
        }

        public async Task UpdateCertificate(Certificate certificate)
        {
            var cert = await GetCertificate(certificate.CertId);
            if (cert != null)
                await Connection.InsertOrReplaceWithChildrenAsync(certificate, true);
        }

        public async Task DeleteCertificate(string certId)
        {
            var cert = await GetCertificate(certId);
            if(cert != null)
                await Connection.DeleteAsync(cert, true);
        }
    }
}