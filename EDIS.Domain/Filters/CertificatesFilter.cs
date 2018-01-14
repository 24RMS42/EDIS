using System;
using Newtonsoft.Json;

namespace EDIS.Domain
{
    public class CertificatesFilter
    {
        [JsonProperty("cert_type")]
        public string CertType { get; set; }

        [JsonProperty("cert_status")]
        public string CertStatus { get; set; }

        [JsonProperty("cert_status_from")]
        public DateTime? CertStatusFrom { get; set; }

        [JsonProperty("cert_status_to")]
        public DateTime? CertStatusTo { get; set; }
    }
}