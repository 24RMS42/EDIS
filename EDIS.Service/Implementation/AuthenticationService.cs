using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces; 

namespace EDIS.Service.Implementation
{ 
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestProvider _requestProvider;

        public AuthenticationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<BasicServiceResponse> LoginAsync(string username, string password, bool rememberMe)
        {
            var url = GlobalSettings.BaseURL + "/login";

            var request = new LoginRequest
            {
                UserEmail = username,
                UserPassword = password
            };

            var response = await _requestProvider.TokenPostAsync(url, request);

            Settings.AccessToken = response.Token;
            Settings.Username = username;

            if (rememberMe)
            {
                Settings.Password = password;
            }

            return new BasicServiceResponse{ Success = true };
        }

        public Task LogoutAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}