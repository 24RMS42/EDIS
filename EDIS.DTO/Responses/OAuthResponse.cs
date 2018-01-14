using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class OAuthResponse : BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}