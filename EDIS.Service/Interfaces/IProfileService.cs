using System.Threading.Tasks;
using EDIS.Domain.Profile;
using EDIS.Shared.Models;

namespace EDIS.Service.Interfaces
{
    public interface IProfileService
    {
        Task<User> GetProfile(string email);
        Task<ServiceResult> GetProfileDetails(string userId);
        Task<User> GetUser(string userId);
        Task<Instrument> GetInstrument(string userId);
        Task<ServiceResult> SaveUserBasicInfo(User user);
        Task<ServiceResult> SaveUserElectricianInfo(User user, Instrument instrument);
    }
}