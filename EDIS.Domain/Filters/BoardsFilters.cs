using Newtonsoft.Json;

namespace EDIS.Domain
{
    public class BoardsFilters
    {
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