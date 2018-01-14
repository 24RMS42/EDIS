using EDIS.Domain.Base;
using EDIS.Domain.Estates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.Buildings
{
    public class BuildingRow : BaseEntity
    {
        [JsonProperty("building_id")]
        [PrimaryKey]
        public string BuildingId { get; set; }

        [JsonIgnore]
        [ForeignKey(typeof(EstateRow))]
        public string EstateId { get; set; }

        [JsonProperty("building_name")]
        public string BuildingName { get; set; }

        [JsonProperty("building_privileges")]
        public string BuildingPrivileges { get; set; }

        [JsonIgnore]
        [ManyToOne]
        public EstateRow Estate { get; set; }
    }
}