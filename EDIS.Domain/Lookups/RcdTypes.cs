using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Lookups
{
    public class RcdTypes : BaseEntity
    {
        [JsonProperty("rcd_type_id")]
        [PrimaryKey]
        public string RcdTypeId { get; set; }

        [JsonProperty("rcd_type")]
        public string RcdType { get; set; }

        [JsonProperty("rcd_type_seq_number")]
        public int RcdTypeSeqNumber { get; set; }

        [JsonProperty("default_rcd_type")]
        public int DefaultRcdType { get; set; }
    }
}