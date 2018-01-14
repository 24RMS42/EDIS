using System.Collections.Generic;
using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class BoardRequest : BaseRequest
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("board_ids")]
        public IEnumerable<string> BoardIds { get; set; }
    }
}