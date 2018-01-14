using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Lookups
{
    public class RcdOpCurrents : BaseEntity
    {
        [JsonProperty("rcd_op_current_id")]
        [PrimaryKey]
        public string RcdOpCurrentId { get; set; }
        [JsonProperty("rcd_op_current")]
        public string RcdOpCurrent { get; set; }
        [JsonProperty("rcd_op_current_seq_number")]
        public int RcdOpCurrentSeqNumber { get; set; }
        [JsonProperty("default_rcd_op_current")]
        public int DefaultRcdOpCurrent { get; set; }
    }
}