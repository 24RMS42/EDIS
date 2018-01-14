using System.Collections.Generic;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;

namespace EDIS.DTO.Responses
{
    public class BoardResponse : BaseResponse
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("boards_requested")]
        public int BoardsRequested { get; set; }

        [JsonProperty("boards_found")]
        public int BoardsFound { get; set; }

        [JsonProperty("boards_returned")]
        public int BoardsReturned { get; set; }

        [JsonProperty("tbl_board")]
        public IEnumerable<Board> Boards { get; set; }

        [JsonProperty("tbl_circuit")]
        public IEnumerable<Circuit> Circuits { get; set; }

        [JsonProperty("tbl_circuit_points_rcd")]
        public IEnumerable<CircuitPointsRcd> CircuitsPointsRcd { get; set; }
    }
}