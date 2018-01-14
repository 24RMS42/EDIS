using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class DeleteCertificateRequest : BaseRequest
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("cert_id")]
        public string CertId { get; set; }
    }
}