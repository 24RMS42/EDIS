using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Lookups
{
    public class NamingConventions : BaseEntity
    {
        [JsonProperty("naming_convention_id")]
        [PrimaryKey]
        public string NamingConventionId { get; set; }

        [JsonProperty("naming_convention")]
        public string NamingConvention { get; set; }

        [JsonProperty("naming_convention_seq_number")]
        public int NamingConventionSeqNumber { get; set; }
    }
}