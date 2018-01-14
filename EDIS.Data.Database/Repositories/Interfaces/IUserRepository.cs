using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Profile;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(string userId);
        Task<string> GetUserLogoImageWithPath(User user);
        Task UpdateUser(User user);
    }
}