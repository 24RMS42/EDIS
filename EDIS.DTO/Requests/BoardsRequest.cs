using System.Collections.Generic;
using EDIS.Domain;
using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class BoardsRequest : BaseRequest
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("filters")]
        public BoardsFilters Filters { get; set; }

        [JsonProperty("limit")]
        public IEnumerable<int> Limit { get; set; }
    }
}