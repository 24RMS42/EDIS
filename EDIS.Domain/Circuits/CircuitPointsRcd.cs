using EDIS.Domain.Boards;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Circuits
{
    public class CircuitPointsRcd : CircuitPointsRcdBase
    {
        [JsonProperty("cpr_id")]
        [PrimaryKey]
        public string CprId { get; set; }

        [JsonProperty("board_id")]
        [ForeignKey(typeof(Board))]
        public string BoardId { get; set; }
    }
}