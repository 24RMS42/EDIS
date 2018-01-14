using Newtonsoft.Json;

namespace EDIS.DTO.Requests
{
    public class EstatesLookupsRequest : BaseRequest
    {
        [JsonProperty("estate_id")]
        public string EstateId { get; set; }
    }
}
