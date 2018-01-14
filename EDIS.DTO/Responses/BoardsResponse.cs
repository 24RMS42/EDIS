using System.Collections.Generic;
using EDIS.Domain;
using EDIS.Domain.Boards;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class BoardsResponse : BaseResponse
    {
        [JsonProperty("building_id")]
        public string BuidlingId { get; set; }

        //[JsonProperty("filters")]
        //public Filter Filters { get; set; }

        [JsonProperty("limit")]
        public IEnumerable<int> Limit { get; set; }

        [JsonProperty("boards_total")]
        public int BoardTotal { get; set; }

        [JsonProperty("boards_found")]
        public int BoardFound { get; set; }

        [JsonProperty("boards_returned")]
        public int BoardReturned { get; set; }

        [JsonProperty("boards")]
        public IEnumerable<BoardRow> Boards { get; set; }
    }
}