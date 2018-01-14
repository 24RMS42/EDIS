using System.Collections.Generic;
using EDIS.Domain.Lookups;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class LookupsResponse : BaseResponse
    {
        [JsonProperty("lookups")]
        public Lookups Lookups { get; set; }
    }
}
