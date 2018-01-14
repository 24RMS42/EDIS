using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class Phases : BaseEntity
    {
        [JsonProperty("phase_value")]
        [PrimaryKey]
        public string PhaseValue { get; set; }
    }
}
