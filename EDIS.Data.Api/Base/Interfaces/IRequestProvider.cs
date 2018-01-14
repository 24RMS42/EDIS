using System.Threading.Tasks;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;

namespace EDIS.Data.Api.Base.Interfaces
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);

        Task<TResult> PostAsync<TResult>(string uri, TResult data);

        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data);

        Task<OAuthResponse> TokenPostAsync(string uri, LoginRequest data);

        Task<TResult> PutAsync<TResult>(string uri, TResult data);

        Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data);
    }
}