using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class LoginRequest
    {
        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("user_password")]
        public string UserPassword { get; set; }
    }
}