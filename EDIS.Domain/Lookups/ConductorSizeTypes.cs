using EDIS.Domain.Base;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Lookups
{
    public class ConductorSizeTypes : BaseEntity
    {
        [JsonProperty("conductor_size_type")]
        [PrimaryKey]
        public string ConductorSizeType { get; set; }

        [JsonProperty("conductor_size_type_desc")]
        public string ConductorSizeTypeDesc { get; set; }

        [JsonIgnore]
        public string ConductorSizeTypeRepresentation => ConductorSizeType + " - " + ConductorSizeTypeDesc;
    }
}