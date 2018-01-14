using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class BaseResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}