using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class UploadCertificateResponse : BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}