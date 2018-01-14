using EDIS.Domain.Base;
using EDIS.Domain.Estates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EDIS.Domain.EstatesLookups
{
    public class ObservationFrom : BaseEntity
    {
        [JsonProperty("obs_from_id")]
        [PrimaryKey]
        public string ObsFromId { get; set; }

        [JsonProperty("estate_id")]
        [ForeignKey(typeof(EstateRow))]
        public string EstateId { get; set; }

        [JsonProperty("obs_cat_code")]
        public string ObsCatCode { get; set; }

        [JsonProperty("obs_from_title")]
        public string ObsFromTitle { get; set; }

        [JsonProperty("obs_from_status")]
        public int ObsFromStatus { get; set; }

        [JsonProperty("obs_from_seq_number")]
        public int? ObsFromSeqNumber { get; set; }

        [JsonProperty("default_observation")]
        public int DefaultObservation { get; set; }

    }
}
