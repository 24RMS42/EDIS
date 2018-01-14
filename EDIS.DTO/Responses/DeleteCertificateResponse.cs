using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class DeleteCertificateResponse : BaseResponse
    {
        [JsonProperty("cert_id")]
        public string CertId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}