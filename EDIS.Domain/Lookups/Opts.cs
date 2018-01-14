using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class Opts : BaseEntity
    {
        [JsonProperty("OPT_value")]
        [PrimaryKey]
        public string OptValue { get; set; }
    }
}
