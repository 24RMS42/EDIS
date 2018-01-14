using EDIS.Domain.Base;
using EDIS.Domain.Estates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.EstatesLookups
{
    public class ObservationGroup : BaseEntity
    {
        [JsonProperty("obs_group_id")]
        [PrimaryKey]
        public string ObsGroupId { get; set; }

        [JsonProperty("estate_id")]
        [ForeignKey(typeof(EstateRow))]
        public string EstateId { get; set; }

        [JsonProperty("obs_group_description")]
        public string ObsGroupDescription { get; set; }
    }
}
