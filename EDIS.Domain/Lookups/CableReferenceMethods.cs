using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class CableReferenceMethods : BaseEntity
    {
        [JsonProperty("ref_method_id")]
        [PrimaryKey]
        public int RefMethodId { get; set; }

        [JsonProperty("ref_method_item_number")]
        public int RefMethodItemNumber { get; set; }

        [JsonProperty("ref_method_description")]
        public string RefMethodDescription { get; set; }

        [JsonProperty("ref_method_number")]
        public string RefMethodNumber { get; set; }

        [JsonIgnore]
        public string RefMethodRepresentation => RefMethodItemNumber + " - Method " + RefMethodNumber + "- " + RefMethodDescription;
    }
}
