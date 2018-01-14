using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Boards
{
    public class BoardRow : BaseEntity
    {
		[JsonProperty("board_id")]
        [PrimaryKey]
        public string BoardId { get; set; }
        
		[JsonProperty("board_reference")]
        public string BoardReference { get; set; }
        
		[JsonProperty("board_reference_type")]
        public string BoardReferenceType { get; set; }

        [JsonIgnore]
        public string BoardIdentity => BoardReference + " " + BoardReferenceType;

        [JsonProperty("board_type")]
        public string BoardType { get; set; }
        
		[JsonProperty("board_function")]
        public string BoardFunction { get; set; }
        
		[JsonProperty("board_location_block")]
        public string BoardLocationBlock { get; set; }
        
		[JsonProperty("board_location_floor")]
        public string BoardLocationFloor { get; set; }
        
		[JsonProperty("board_location")]
        public string BoardLocation { get; set; }
    }
}