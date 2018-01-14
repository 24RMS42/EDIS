using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Lookups
{
    public class CsaCpc : BaseEntity
    {
        [JsonProperty("csa_cpc_id")]
        [PrimaryKey]
        public string CsaCpcId { get; set; }

        [JsonProperty("csa_cpc")]
        public string CsaCpcValue { get; set; }
    }
}