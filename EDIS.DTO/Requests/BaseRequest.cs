using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class BaseRequest
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}