using System.Collections.Generic;
using EDIS.Domain.Estates;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class EstatesResponse : BaseResponse
    {
        [JsonProperty("estates")]
        public IEnumerable<EstateRow> Estates { get; set; }
    }
}