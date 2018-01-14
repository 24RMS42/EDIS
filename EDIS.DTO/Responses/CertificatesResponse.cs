using System.Collections.Generic;
using EDIS.Domain;
using EDIS.Domain.Certificates;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class CertificatesResponse : BaseResponse
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        //[JsonProperty("filters")]
        //public CertificatesFilter Filters { get; set; }

        [JsonProperty("limit")]
        public IEnumerable<string> Limit { get; set; }

        [JsonProperty("certificates_total")]
        public int CertificatesTotal { get; set; }

        [JsonProperty("certificates_found")]
        public int CertificatesFound { get; set; }

        [JsonProperty("certificates_returned")]
        public int CertificatesReturned { get; set; }

        [JsonProperty("certificates")]
        public IEnumerable<CertificateRow> Certificates { get; set; }
    }
}