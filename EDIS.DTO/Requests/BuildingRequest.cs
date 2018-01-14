using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class BuildingRequest : BaseRequest
    {
        [JsonProperty("building_id")]
        public string BuildingId { get; set; }
    }
}