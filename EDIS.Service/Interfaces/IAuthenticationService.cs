using System.Threading.Tasks;
using EDIS.DTO.Responses;
using EDIS.Service.Base;

namespace EDIS.Service.Interfaces
{
    public interface IAuthenticationService
    {
        //Task<BasicResponse> IsAuthenticated(string username = "", string password = "");

        Task<BasicServiceResponse> LoginAsync(string userName, string password, bool rememberMe);

        Task LogoutAsync();
    }
}