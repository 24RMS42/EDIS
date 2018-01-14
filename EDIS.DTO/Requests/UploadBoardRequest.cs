using System.Collections.Generic;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;
using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class UploadBoardRequest : BaseRequest
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }

        [JsonProperty("tbl_board")]
        public IEnumerable<Board> Boards { get; set; }

        [JsonProperty("tbl_circuit")]
        public IEnumerable<Circuit> Circuits { get; set; }

        [JsonProperty("tbl_circuit_points_rcd")]
        public IEnumerable<CircuitPointsRcd> CircuitsPointsRcd { get; set; }
    }
}