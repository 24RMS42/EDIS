using System.Collections.Generic;
using EDIS.Domain.EstatesLookups;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class EstatesLookupsResponse : BaseResponse
    {
        [JsonProperty("estate_id")]
        public string EstateId { get; set; }

        [JsonProperty("obsFromLookups")]
        public ObservationFromContainer ObservationFromEncloser { get; set; }

        [JsonProperty("obsGroup")]
        public ObservationGroupContainer ObservationGroupEncloser { get; set; }
    }
}
