using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;

namespace EDIS.Domain.Lookups
{
    public class CsaLive : BaseEntity
    {
        [JsonProperty("csa_live_id")]
        [PrimaryKey]
        public string CsaLiveId { get; set; }

        [JsonProperty("csa_live")]
        public string CsaLiveValue { get; set; }
    }
}