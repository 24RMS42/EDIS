using EDIS.Domain.Base;
using EDIS.Domain.Estates;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace EDIS.Domain.EstatesLookups
{
    public class ObservationGroupContainer : BaseEntity
    {
        [JsonProperty("tbl_observation_group")]
        public List<ObservationGroup> ObservationGroupItems { get; set; }

        
    }
}
